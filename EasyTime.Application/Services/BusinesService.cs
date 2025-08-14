using AutoMapper;
using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Application.Contract.Dtos.BusinessServices;
using EasyTime.Application.Contract.Dtos.Category;
using EasyTime.Application.Contract.Dtos.Service;
using EasyTime.Application.Contract.IServices;
using EasyTime.InfraStracure.UnitOfWork;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using EasyTime.Utilities.Convertor;
using Microsoft.EntityFrameworkCore;

namespace EasyTime.Application.Services
{
    public class BusinesService(IBaseRepository<long, BusinesCity> cityRepository, IBaseRepository<long, UserBusinessOwner> userBusinessOwnerRepository, IBaseRepository<long, BusinesRegion> regionRepository, IBaseRepository<long, BusinesNeighberhood> neighberhoodRepository, IBaseRepository<long, Business> businessRepository, IBaseRepository<Guid, User> userRepository, IBaseRepository<long, BusinessOwnerDay> businessOwnerDayRepository, IBaseRepository<long, BusinessOwnerTime> businessOwnerTimeRepository, IBaseRepository<Guid, User> businessOwnerRepository, IBaseRepository<long, Reserve> reserveRepository, IMapper mapper, IBaseRepository<int, Category> categoryRepository, IBaseRepository<long, Service> serviceRepository, IBaseRepository<long, BusinessServices> businessServiceRepository) : IBusinesService, IService
    {
        private readonly IBaseRepository<long, BusinesCity> cityRepository = cityRepository;
        private readonly IBaseRepository<long, BusinesRegion> regionRepository = regionRepository;
        private readonly IBaseRepository<long, BusinesNeighberhood> neighberhoodRepository = neighberhoodRepository;
        private readonly IBaseRepository<Guid, User> userRepository = userRepository;
        private readonly IBaseRepository<long, Business> businessRepository = businessRepository;
        private readonly IBaseRepository<long, BusinessOwnerDay> businessOwnerDayRepository = businessOwnerDayRepository;
        private readonly IBaseRepository<long, UserBusinessOwner> userBusinessOwnerRepository = userBusinessOwnerRepository;
        private readonly IBaseRepository<long, BusinessOwnerTime> businessOwnerTimeRepository = businessOwnerTimeRepository;
        private readonly IBaseRepository<Guid, User> businessOwnerRepository = businessOwnerRepository;
        private readonly IBaseRepository<long, Reserve> reserveRepository = reserveRepository;
        private readonly IBaseRepository<int, Category> categoryRepository;
        private readonly IBaseRepository<long, Service> serviceRepository;
        private readonly IBaseRepository<long, EasyTime.Model.Models.BusinessServices> businessServiceRepository;
        private readonly IMapper mapper = mapper;

        public async Task<List<BusinessDto>> FilterBusinesses(
            long neighborhoodId,
            int skip = 0,
            int take = 6,
            float? maxAmount = null,
            int? categoryId = null)
        {
            var bq = await businessRepository.GetAllEntities();
            var bsq = await businessServiceRepository.GetAllEntities();

            if (maxAmount is null)
            {
                maxAmount = await bsq.Where(s => s.Business.NeighberhoodId == neighborhoodId)
                                     .Select(s => s.Amount)
                                     .DefaultIfEmpty()
                                     .MaxAsync();
            }
            var q =
                from b in bq
                join bs in bsq on b.Id equals bs.BusinessId
                where b.NeighberhoodId == neighborhoodId
                      && bs.Amount >= 0
                      && bs.Amount <= maxAmount
                select b;

            if (categoryId is not null && categoryId != 0)
                q = q.Where(b => b.CategoryId == categoryId);

            var result = await q.AsNoTracking()
                                .Distinct()
                                .OrderBy(b => b.Id)
                                .Skip(skip)
                                .Take(take)
                                .Select(b => new BusinessDto
                                {
                                    Id = b.Id,
                                    BusinessName = b.Name,
                                    BusinessOwnerName = b.User.UserName,
                                    BusinessLogo = "Images/" + b.Logo
                                })
                                .ToListAsync();

            return result;
        }


        public async Task<BusinessPlaceDto> FilterBusinesByPlace(long? businesCityId, long? regionId)
        {
            var cities = await cityRepository.GetAllEntities();
            var regions = await regionRepository.GetAllEntities();
            var neighborhoods = await neighberhoodRepository.GetAllEntities();

            var selectedCity = cities.FirstOrDefault(c => c.Id == businesCityId);
            if (selectedCity == null)
                return null;

            var selectedRegions = regions.Where(r => r.BusinesCityId == businesCityId).ToList();

            var regionDtos = selectedRegions.Select(region => new RegionDto
            {
                Id = region.Id,
                RegionName = region.Name,
                Neighborhoods = neighborhoods
                                 .Where(n => n.BusinesRegionId == region.Id)
                                 .Select(n => new NeighborhoodDto
                                 {
                                     Id = n.Id,
                                     NeighborhoodName = n.Name
                                 })
                                 .ToList()
            }).ToList();
            var selectedregion = regions.FirstOrDefault(x => x.Id == regionId);

            var selectedNeighberhoods = neighborhoods.Where(x => x.BusinesRegionId == regionId).ToList();

            var neighberhoodsDtos = selectedNeighberhoods.Select(x => new NeighborhoodDto()
            {
                Id = x.Id,
                NeighborhoodName = x.Name
            }).ToList();

            var result = new BusinessPlaceDto
            {
                CityId = selectedCity.Id,
                CityName = selectedCity.Name,
                Regions = regionDtos,
                Neighborhoods = neighberhoodsDtos
            };

            return result;
        }


        public async Task<BusinessDetailDto?> GetBusinessDetail(long businessId)
        {
            var days = await businessOwnerDayRepository.GetAllEntities();
            var times = await businessOwnerTimeRepository.GetAllEntities();
            // کوئری ترکیبی با join کامل
            var result = await (from b in await businessRepository.GetAllEntities()
                                join c in await cityRepository.GetAllEntities()
                                    on b.CityId equals c.Id
                                join r in await regionRepository.GetAllEntities()
                                    on c.Id equals r.BusinesCityId
                                join n in await neighberhoodRepository.GetAllEntities()
                                    on r.Id equals n.BusinesRegionId
                                join u in await userRepository.GetAllEntities()
                                    on b.BusinessOwnerId equals u.Id
                                where b.Id == businessId
                                select new BusinessDetailDto()
                                {
                                    Logo = b.Logo,
                                    Name = b.Name,
                                    Description = b.Description,
                                    CityId = c.Id,
                                    CityName = c.Name,
                                    NeighberhoodId = n.Id,
                                    NeighberhoodName = n.Name,
                                    RegionId = r.Id,
                                    RegionName = r.Name,
                                    UserId = b.BusinessOwnerId,
                                    BusinessId = b.Id,
                                    BusinessOwnerDayDtos = days.Select(x => new BusinessOwnerDayDto()
                                    {
                                        Id = x.Id,
                                        DayOfWeek = x.DayOfWeek,
                                        BusinessOwnerTimes = times.Where(t => t.BusinessOwnerDayId == x.Id).Where(t => t.IsReserved == true || t.IsReserved == false).Select(t => new BusinessOwnerTimeDto()
                                        {
                                            From = t.From,
                                            To = t.To,
                                            Id = t.Id,
                                            IsReserved = t.IsReserved
                                        }).ToList()
                                    }).ToList()
                                }).FirstOrDefaultAsync();
            return result;
        }


        [UnitOfWork]
        public async Task<Result<ReserveDto>> Reserve(ReserveDto dto)
        {
            var validateReserve = await ValidateReserve(dto);
            if (validateReserve.IsSuccess)
            {
                var resreve = mapper.Map<Reserve>(dto);
                var userEntities = await userRepository.GetAllEntities();
                var user = await userEntities.Where(x => x.IsBusinesOwner == false && x.RoleId == 3 && x.Id == dto.UserId).FirstOrDefaultAsync();
                var businessOwner = await userEntities.Where(x => x.IsBusinesOwner == true && x.Id == dto.BusinessOwnerId).FirstOrDefaultAsync();

                var reserve = new Reserve()
                {
                    BusinessOwnerDayId = dto.BusinessOwnerDayId,
                    BusinessOwnerTimeId = dto.BusinessOwnerTimeId,
                    UserId = dto.UserId,
                    IsDelete = false,
                    UpdateEntityDate = DateTime.UtcNow
                };
                await reserveRepository.Add(reserve);
                var userBisinessOwner = new UserBusinessOwner()
                {
                    UserId = user.Id,
                    BusinessOnwerId = businessOwner.Id
                };
                await userBusinessOwnerRepository.Add(userBisinessOwner);
                await reserveRepository.SaveChanges();
                return Result<ReserveDto>.Success("رزرو شما با موفقیت انجام شد");
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
        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            var cities = await cityRepository.GetAllEntities();

            return cities.Select(c => new CityDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await categoryRepository.
                GetAllEntities();
            var result = categories.Select(x => new CategoryDto()
            {
                CategoryId = x.Id,
                Name = x.Name
            }).ToList();
            return result;
        }

        public async Task<List<ServiceDto>> GetServicseWithCategory(int categoryId)
        {
            var services = await serviceRepository.
                 GetAllEntities();
            var result = services
                .Where(x => x.CategoryId == categoryId).Select(x => new ServiceDto()
                {
                    CategoryId = x.CategoryId,
                    ServiceId = x.Id,
                    ServiceName = x.ServiceName
                }).ToList();
            return result;
        }

        public async Task<float> GetBusinessServicesAmount()
        {
            var busineses = await businessServiceRepository.GetAllEntities();
            var maxAmount = busineses.Max(x => x.Amount);
            return maxAmount;

        }
    }
}