using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using Microsoft.EntityFrameworkCore;
namespace EasyTime.Application.Services
{
    public class BusinesService : IBusinesService
    {
        private readonly IBaseRepository<long, BusinesCity> cityRepository;
        private readonly IBaseRepository<long, BusinesRegion> regionRepository;
        private readonly IBaseRepository<long, BusinesNeighberhood> neighberhoodRepository;
        private readonly IBaseRepository<Guid, User> userRepository;
        private readonly IBaseRepository<long, Business> businessRepository;
        private readonly IBaseRepository<long, BusinessOwnerDay> businessOwnerDayRepository;
        private readonly IBaseRepository<long, BusinessOwnerTime> businessOwnerTimeRepository;
        public BusinesService(IBaseRepository<long, BusinesCity> cityRepository, IBaseRepository<long, BusinesRegion> regionRepository, IBaseRepository<long, BusinesNeighberhood> neighberhoodRepository, IBaseRepository<long, Business> businessRepository, IBaseRepository<Guid, User> userRepository, IBaseRepository<long, BusinessOwnerDay> businessOwnerDayRepository, IBaseRepository<long, BusinessOwnerTime> businessOwnerTimeRepository)
        {
            this.cityRepository = cityRepository;
            this.regionRepository = regionRepository;
            this.neighberhoodRepository = neighberhoodRepository;
            this.businessRepository = businessRepository;
            this.userRepository = userRepository;
            this.businessOwnerDayRepository = businessOwnerDayRepository;
            this.businessOwnerTimeRepository = businessOwnerTimeRepository;
        }

        public IQueryable<BusinessDto> FilterBusines(long businesCityId)
        {
            var result = (from p in FilterBusinesByPlace(businesCityId)
                          join b in businessRepository.GetAllEntities()
                             on p.CityId equals b.CityId
                          join u in userRepository.GetAllEntities()
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

        public IQueryable<BusinessPlaceDto> FilterBusinesByPlace(long businesCityId)
        {
            var result = from c in cityRepository.GetAllEntities()
                         join r in regionRepository.GetAllEntities()
                            on c.Id equals r.BusinesCityId
                         join n in neighberhoodRepository.GetAllEntities()
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

        public BusinessDetailDto GetBusinessDetail(long businessId)
        {
            var result = businessRepository.GetAllEntities()
                .Include(x => x.BusinesCity)
                .Include(x => x.BusinesRegion)
                .Include(x => x.BusinesNeighberhood)
                .Where(x => x.Id == businessId)
                .Select(x => new BusinessDetailDto()
                {
                    BusinesOwnerId = x.BusinesOwnerId,
                    BusinessId = x.Id,
                    CityId = x.CityId,
                    Description = x.Description,
                    Logo = x.Logo,
                    Name = x.Name,
                    NeighberhoodId = x.NeighberhoodId,
                    CityName = x.BusinesCity.Name,
                    RegionName = x.BusinesRegion.Name,
                    NeighberhoodName = x.BusinesNeighberhood.Name,
                    RegionId = x.RegionId,
                    BusinessOwnerDayDtos = businessOwnerDayRepository.GetAllEntities().Select(z=> new BusinessOwnerDayDto()
                    {
                        DayOfWeek = z.DayOfWeek,
                        BusinessOwnerTimes = businessOwnerTimeRepository.GetAllEntities().Where(b=> b.BusinessOwnerDayId == z.Id).Select(bt=> new BusinessOwnerTimeDto()
                        {
                            From = bt.From,
                            IsReserved = bt.IsReserved,
                            To = bt.To
                        }).ToList()
                    }).ToList()
                }).FirstOrDefault();
            return result;
        }
    }
}
