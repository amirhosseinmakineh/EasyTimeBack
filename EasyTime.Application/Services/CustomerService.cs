using EasyTime.Application.Contract.Dtos.Customer;
using EasyTime.Application.Contract.Enums;
using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using EasyTime.Utilities.Sender;
using Microsoft.EntityFrameworkCore;

namespace EasyTime.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IBaseRepository<Guid, User> userRepository;
        private readonly IBaseRepository<long, Business> businessRepository;
        private readonly IBaseRepository<long, BusinessOwnerDay> dayRepository;
        private readonly IBaseRepository<long, BusinessOwnerTime> timeRepository;
        private readonly IBaseRepository<long, Reserve> reserveRepository;
        private readonly IBaseRepository<long, UserBusinessOwner> userBusinessOwnerRepository;
        private readonly SmsSender smsSender;

        public CustomerService(IBaseRepository<Guid, User> userRepository, IBaseRepository<long, Business> businessRepository, IBaseRepository<long, BusinessOwnerDay> dayRepository, IBaseRepository<long, BusinessOwnerTime> timeRepository, IBaseRepository<long, Reserve> reserveRepository, IBaseRepository<long, UserBusinessOwner> userBusinessOwnerRepository, SmsSender smsSender)
        {
            this.userRepository = userRepository;
            this.businessRepository = businessRepository;
            this.dayRepository = dayRepository;
            this.timeRepository = timeRepository;
            this.reserveRepository = reserveRepository;
            this.userBusinessOwnerRepository = userBusinessOwnerRepository;
            this.smsSender = smsSender;
        }

        public async Task<List<CustomerDto>> GetAllCustomer(Guid businessOwnerId)
        {
            var users = await userRepository.GetAllEntities();
            var business = await businessRepository.GetAllEntities();
            var days = await dayRepository.GetAllEntities();
            var times = await timeRepository.GetAllEntities();
            var userBusinessOwner = await userBusinessOwnerRepository.GetAllEntities();
            var reserves = await reserveRepository.GetAllEntities();
            var customers = await (from u in users
                            join ub in userBusinessOwner 
                                on u.Id equals ub.UserId
                            join r in reserves
                                on ub.UserId equals r.UserId
                            join d in days 
                                on r.BusinessOwnerDayId equals d.Id
                            join t in times 
                                on r.BusinessOwnerTimeId equals t.Id
                                   where ub.BusinessOnwerId == businessOwnerId
                            select new CustomerDto()
                            {
                                CustomerId = u.Id,
                                UserName = u.UserName,
                                DayId = d.Id,
                                TimeId = t.Id,
                                DayName = 
                                d.Id == 0 ? DayOfWeek.Sunday.ToString() : 
                                d.Id == 1 ? DayOfWeek.Monday.ToString() : 
                                d.Id == 2 ? DayOfWeek.Tuesday.ToString() : 
                                d.Id == 3 ? DayOfWeek.Wednesday.ToString() :
                                d.Id == 4 ? DayOfWeek.Thursday.ToString() :
                                d.Id == 5 ? DayOfWeek.Friday.ToString() :
                                d.Id == 6 ? DayOfWeek.Saturday.ToString() :
                                DayOfWeek.Sunday.ToString(),
                                From = t.From,
                                To = t.To,

                            }).ToListAsync();
            return customers;
        }

        public async Task SendSmsForCustomer(List<Guid> customerIdes)
        {
            var users = await userRepository.GetAllEntities();
            var reserves = await reserveRepository.GetAllEntities();
            var times = await timeRepository.GetAllEntities();
            var findUsersTimes = from u in users
                                 join r in reserves
                                    on u.Id equals r.UserId
                                 join t in times
                                    on r.BusinessOwnerTimeId equals t.Id
                                 select new 
                                 {
                                     u.Id,
                                     r.UserId,
                                     r.BusinessOwnerTimeId,
                                     t.From,
                                     u.MobileNumber
                                 };

            var time = DateTime.Now.TimeOfDay;
            var userForSms = (from c in customerIdes
                              join s in findUsersTimes
                                 on c equals s.UserId
                              where time >= s.From - TimeSpan.FromMinutes(30) && time < s.From
                              select c)
                              .ToList();

            foreach(var item in findUsersTimes)
            {
                await smsSender.SendSms(item.MobileNumber,"30 دیقیقه تا زمان آرایشگاه شما باقی مانده است");
            }


        }
    }
}