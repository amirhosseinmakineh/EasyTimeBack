using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using EasyTime.Utilities.Convertor;
using Microsoft.EntityFrameworkCore;
namespace EasyTime.Application.Services
{
    public class BusinesService(IBaseRepository<long, BusinesCity> cityRepository, IBaseRepository<long, BusinesRegion> regionRepository, IBaseRepository<long, BusinesNeighberhood> neighberhoodRepository, IBaseRepository<long, Business> businessRepository, IBaseRepository<Guid, User> userRepository, IBaseRepository<long, BusinessOwnerDay> businessOwnerDayRepository, IBaseRepository<long, BusinessOwnerTime> businessOwnerTimeRepository, IBaseRepository<Guid, BusinessOwner> businessOwnerRepository, IBaseRepository<long, Reserve> reserveRepository) : IBusinesService
    {
        private readonly IBaseRepository<long, BusinesCity> cityRepository = cityRepository;
        private readonly IBaseRepository<long, BusinesRegion> regionRepository = regionRepository;
        private readonly IBaseRepository<long, BusinesNeighberhood> neighberhoodRepository = neighberhoodRepository;
        private readonly IBaseRepository<Guid, User> userRepository = userRepository;
        private readonly IBaseRepository<long, Business> businessRepository = businessRepository;
        private readonly IBaseRepository<long, BusinessOwnerDay> businessOwnerDayRepository = businessOwnerDayRepository;
        private readonly IBaseRepository<long, BusinessOwnerTime> businessOwnerTimeRepository = businessOwnerTimeRepository;
        private readonly IBaseRepository<Guid, BusinessOwner> businessOwnerRepository = businessOwnerRepository;
        private readonly BaseService<ReserveDto, long, Reserve> reserveService;
        private readonly IBaseRepository<long, Reserve> reserveRepository;

        public async Task<IQueryable<BusinessDto>> FilterBusines(long businesCityId)
        {
            var result = (from p in await FilterBusinesByPlace(businesCityId)
                          join b in await businessRepository.GetAllEntities()
                             on p.CityId equals b.CityId
                          join u in await userRepository.GetAllEntities()
                             on b.BusinesOwnerId equals u.Id
                          where b.CityId == p.CityId && b.RegionId == p.RegionId && b.NeighberhoodId == p.NeighberhoodId
                          select new BusinessDto()
                          {
                              BusinessLogo = b.Logo,
                              BusinessName = b.Name,
                              Id = b.Id,
                              BusinessOwnerName = u.UserName
                          }
                         ).Take(4).Skip(1);

            return result;
        }

        public async Task<IQueryable<BusinessPlaceDto>> FilterBusinesByPlace(long businesCityId)
        {
            var result = from c in await cityRepository.GetAllEntities()
                         join r in await regionRepository.GetAllEntities()
                            on c.Id equals r.BusinesCityId
                         join n in await neighberhoodRepository.GetAllEntities()
                            on r.Id equals n.BusinesRegionId
                         where c.Id == businesCityId
                         select new BusinessPlaceDto()
                         {
                             CityName = c.Name,
                             RegionName = r.Name,
                             NeighberhoodName = n.Name,
                             CityId = c.Id,
                             RegionId = n.Id,
                             NeighberhoodId = n.Id
                         };
            return result;
        }

        public async Task<BusinessDetailDto> GetBusinessDetail(long businessId)
        {
            var busineses = await businessRepository.GetAllEntities();
            var businessOwners = await businessOwnerRepository.GetAllEntities();
            var cityies = await cityRepository.GetAllEntities();
            var regions = await regionRepository.GetAllEntities();
            var neighberhoods = await neighberhoodRepository.GetAllEntities();
            var businessOwnerDayes = await businessOwnerDayRepository.GetAllEntities();
            var businessOwnerTimes = await businessOwnerTimeRepository.GetAllEntities();
            var result = (from b in busineses
                          join bu in businessOwners
                            on b.BusinesOwnerId equals bu.Id
                          join c in cityies
                            on b.CityId equals c.Id
                          join r in regions
                            on b.RegionId equals r.Id
                          join n in neighberhoods
                            on b.NeighberhoodId equals n.Id

                          where b.Id == businessId
                          select new BusinessDetailDto()
                          {
                              BusinesOwnerId = b.BusinesOwnerId,
                              BusinessId = b.Id,
                              CityId = b.CityId,
                              CityName = c.Name,
                              Description = b.Description,
                              Logo = b.Logo,
                              NeighberhoodId = b.NeighberhoodId,
                              Name = b.Name,
                              NeighberhoodName = n.Name,
                              RegionId = r.Id,
                              RegionName = r.Name,
                              BusinessOwnerDayDtos = (from bd in businessOwnerDayes
                                                      join bt in businessOwnerTimes
                                                       on bd.Id equals bt.BusinessOwnerDayId
                                                      where bt.IsReserved == true && bt.IsReserved == false
                                                      select new BusinessOwnerDayDto()
                                                      {
                                                          DayOfWeek = bd.DayOfWeek,
                                                          BusinessOwnerTimes = new List<BusinessOwnerTimeDto>()
                                                            {
                                                                new BusinessOwnerTimeDto()
                                                                {
                                                                    From = bt.From,
                                                                    To = bt.To,
                                                                    IsReserved = bt.IsReserved
                                                                }
                                                            }
                                                      }).ToList()


                          }
                          ).FirstOrDefault();
            return result;
        }

        public async Task<Result<ReserveDto>> Reserve(ReserveDto dto)
        {
            var validateReserve = await ValidateReserve(dto);
            if (validateReserve.IsSuccess)
            {
                var reserve = new ReserveDto()
                {
                    BusinessOwnerDayId = dto.BusinessOwnerDayId,
                    BusinessOwnerTimeId = dto.BusinessOwnerTimeId,
                    CreateObjectDate = DateTime.Now,
                    Id = dto.Id,
                    IsDelete = dto.IsDelete,
                    UserId = dto.UserId
                };
                await reserveService.Create(reserve);
                return Result<ReserveDto>.Success(reserve, "رزرو شما با موفقیت انجام شد");
            }
            else
                return Result<ReserveDto>.Failure("زمان انتخاب‌شده قبلاً رزرو شده است.");
        }

        public async Task<Result<bool>> ValidateReserve(ReserveDto dto)
        {
            var reserves = await reserveRepository.GetAllEntities();
            var times = await businessOwnerTimeRepository.GetAllEntities();

            var hasConflict = await (from r in reserves
                                     join bt in times
                                       on r.BusinessOwnerTimeId equals bt.Id
                                     where bt.IsReserved == true
                                     select r).AnyAsync();

            if (hasConflict)
                return Result<bool>.Failure("زمان انتخاب‌شده قبلاً رزرو شده است.");

            return Result<bool>.Success(true);
        }
    }
}
