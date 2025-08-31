using AutoMapper;
using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Application.Contract.Dtos.Category;
using EasyTime.Application.Contract.Dtos.Comments;
using EasyTime.Application.Contract.Dtos.Service;
using EasyTime.Application.Contract.IServices;
using EasyTime.InfraStracure.UnitOfWork;
using EasyTime.Model.Enums;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using EasyTime.Utilities.Convertor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyTime.Application.Services
{
    public class BusinesService(
        IBaseRepository<long, BusinesCity> cityRepository,
        IBaseRepository<long, UserBusinessOwner> userBusinessOwnerRepository,
        IBaseRepository<long, BusinesRegion> regionRepository,
        IBaseRepository<long, BusinesNeighberhood> neighberhoodRepository,
        IBaseRepository<long, Business> businessRepository,
        IBaseRepository<Guid, User> userRepository,
        IBaseRepository<long, BusinessOwnerDay> businessOwnerDayRepository,
        IBaseRepository<long, BusinessOwnerTime> businessOwnerTimeRepository,
        IBaseRepository<Guid, User> businessOwnerRepository,
        IBaseRepository<long, Reserve> reserveRepository,
        IMapper mapper,
        IBaseRepository<int, Category> categoryRepository,
        IBaseRepository<long, Service> serviceRepository,
        IBaseRepository<long, BusinessServices> businessServiceRepository // ← اگر ضروری است nullable نکن
,
        IBaseRepository<long, Achievements> achivmentRepository,
        IBaseRepository<long, Rate> rateRepository,
        IBaseRepository<long, Comment> commentRepository) : IBusinesService, IService
    {
        private readonly IBaseRepository<long, BusinesCity> _cityRepository = cityRepository;
        private readonly IBaseRepository<long, UserBusinessOwner> _userBusinessOwnerRepository = userBusinessOwnerRepository;
        private readonly IBaseRepository<long, BusinesRegion> _regionRepository = regionRepository;
        private readonly IBaseRepository<long, BusinesNeighberhood> _neighberhoodRepository = neighberhoodRepository;
        private readonly IBaseRepository<long, Business> _businessRepository = businessRepository;
        private readonly IBaseRepository<Guid, User> _userRepository = userRepository;
        private readonly IBaseRepository<long, BusinessOwnerDay> _businessOwnerDayRepository = businessOwnerDayRepository;
        private readonly IBaseRepository<long, BusinessOwnerTime> _businessOwnerTimeRepository = businessOwnerTimeRepository;
        private readonly IBaseRepository<Guid, User> _businessOwnerRepository = businessOwnerRepository;
        private readonly IBaseRepository<long, Reserve> _reserveRepository = reserveRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IBaseRepository<int, Category> _categoryRepository = categoryRepository;
        private readonly IBaseRepository<long, Service> _serviceRepository = serviceRepository;
        private readonly IBaseRepository<long, BusinessServices> _businessServiceRepository = businessServiceRepository;
        private readonly IBaseRepository<long, Achievements> _achivmentRepository = achivmentRepository;
        private readonly IBaseRepository<long, Rate> _rateRepository = rateRepository;
        private readonly IBaseRepository<long, Comment> _commentRepository = commentRepository;
        public async Task<List<BusinessDto>> FilterBusinesses(
            long neighborhoodId,
            List<long>? serviceIdes, int? categoryId,
            int skip = 0,
            int take = 6,
            decimal? maxAmount = null
        )
        {
            var bq = await businessRepository.GetAllEntities();
            var ratesQ = await _rateRepository.GetAllEntities();
            var achQ = await _achivmentRepository.GetAllEntities();
            var dayQ = await _businessOwnerDayRepository.GetAllEntities();
            var timeQ = await _businessOwnerTimeRepository.GetAllEntities();
            var busineseServices = await businessServiceRepository.GetAllEntities();
            var services = await serviceRepository.GetAllEntities();

            var q = bq.Where(b => b.NeighberhoodId == neighborhoodId);

            if (categoryId.HasValue && categoryId.Value != 0)
                q = q.Where(b => b.CategoryId == categoryId.Value);

            if (!maxAmount.HasValue)
                q = q.Where(b => b.BusinessServices.Any(bs => bs.Amount >= 0 && bs.Amount <= maxAmount.Value));

            if (serviceIdes != null && serviceIdes.Any())
            {
                var ids = serviceIdes.Distinct().ToArray();
                q = q.Where(b => b.BusinessServices.Any(bs => ids.Contains(bs.ServiceId)));
            }

            var result = await q
                .AsNoTracking()
                .OrderBy(b => b.Id)
                .Skip(skip)
                .Take(take)
                .Select(b => new BusinessDto
                {
                    Id = b.Id,
                    BusinessName = b.Name,
                    BusinessOwnerName = b.User.UserName,
                    BusinessLogo = b.Logo,
                    Description = b.Description,
                    Amount = b.BusinessServices
                             .Select(bs => (decimal?)bs.Amount)
                             .Where(v => v.HasValue)
                             .Min() ?? 0m,

                    UserRate = ratesQ
                                .Where(r => r.UserId == b.BusinessOwnerId && !r.IsDelete)
                                .Average(r => (float?)r.RateNumber) ?? 0f,

                    AchevmentNames = achQ
                        .Where(a => a.UserId == b.User.Id)
                        .Select(a => a.Name)
                        .ToList(),
                    BusinessDayTimeDtos =
                        (from d in dayQ.Where(d => d.BusinessId == b.Id)
                         from t in timeQ.Where(t => t.BusinessOwnerDayId == d.Id).DefaultIfEmpty()
                         select new BusinessDayTimeDto
                         {
                             BusinessOwnerDayId = d.Id,
                             BusinessOwnerTimeId = (int?)t!.Id,
                             DayOfWeek = d.DayOfWeek,
                             From = (TimeSpan?)t!.From,
                             To = (TimeSpan?)t!.To,
                             IsReserved = (bool?)(t != null ? t.IsReserved : (bool?)null) ?? false
                         }).ToList(),

                    ServiceDtos = (serviceIdes != null && serviceIdes.Any())
                        ? (from bs in busineseServices
                           join s in services on bs.ServiceId equals s.Id
                           where bs.BusinessId == b.Id && serviceIdes.Contains(bs.ServiceId)
                           select new ServiceDto
                           {
                               CategoryId = b.Category.Id,
                               ServiceId = s.Id,
                               ServiceName = s.ServiceName,
                               Id = s.Id,


                           }).ToList()
                        : (from bs in busineseServices
                           join s in services on bs.ServiceId equals s.Id
                           where bs.BusinessId == b.Id
                           select new ServiceDto
                           {
                               CategoryId = b.Category.Id,
                               ServiceId = s.Id,
                               ServiceName = s.ServiceName
                           }).ToList()
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
            var businesServices = await _businessServiceRepository.GetAllEntities();
            var comments = await _commentRepository.GetAllEntities();
            var achievements = await _achivmentRepository.GetAllEntities();

            var result = await (from b in await businessRepository.GetAllEntities()
                                join c in await cityRepository.GetAllEntities()
                                    on b.CityId equals c.Id
                                join r in await regionRepository.GetAllEntities()
                                    on c.Id equals r.BusinesCityId
                                join n in await neighberhoodRepository.GetAllEntities()
                                    on r.Id equals n.BusinesRegionId
                                join u in await userRepository.GetAllEntities()
                                    on b.BusinessOwnerId equals u.Id
                                join bs in await _businessServiceRepository.GetAllEntities()
                                    on b.Id equals bs.BusinessId
                                join a in achievements
                                    on u.Id equals a.UserId
                                where b.Id == businessId
                                select new BusinessDetailDto()
                                {
                                    Logo = b.Logo,
                                    BannerName = b.BannerName,
                                    BusinessServiceDtos = businesServices.Include(x => x.Service).Where(x => x.BusinessId == b.Id).Select(x => new BusinessServiceDto()
                                    {
                                        BusinessId = b.Id,
                                        ServiceName = x.Service.ServiceName,
                                        ServiceId = x.Service.Id,
                                        Amount = x.Amount
                                    }).ToList(),
                                    AchievementDtos = achievements.Where(x=> x.UserId == u.Id).Select(x=> new Contract.Dtos.Achevment.AchievementDto()
                                    {
                                        Name = a.Name,
                                        UserId = a.UserId
                                    }).ToList(),
                                    CommentDtos = comments.Where(x=> x.BusinessId == b.Id).Select(x=> new CommentDto()
                                    {
                                        BusinessId = b.Id,
                                        CommentId = x.Id,
                                        CommentText = x.CommentText,
                                        UserId = u.Id
                                    }).ToList(),
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

        public async Task<decimal> GetBusinessServicesAmount()
        {
            var busineses = await businessServiceRepository.GetAllEntities();
            var maxAmount = busineses.Max(x => x.Amount);
            return maxAmount;

        }

        public async Task<SearachDto> GetSelectedPlace(long cityId, long regionId, long neighberHoodId)
        {
            var result = (from c in await cityRepository.GetAllEntities()
                          join r in await regionRepository.GetAllEntities()
                             on c.Id equals r.BusinesCityId
                          join n in await neighberhoodRepository.GetAllEntities()
                             on r.Id equals n.BusinesRegionId
                          where c.IsDelete != false
                          select new SearachDto()
                          {
                              CityId = c.Id,
                              CityName = c.Name,
                              NeighberHoodId = n.Id,
                              NeighberHoodName = n.Name,
                              RegionId = r.Id,
                              RegionName = r.Name
                          }).FirstOrDefault();
            return result;

        }

        public async Task<List<BusinessServiceDto>> GetAllServices()
        {
            var sList = await serviceRepository.GetAllEntities();
            var bsList = await businessServiceRepository.GetAllEntities();
            var bList = await businessRepository.GetAllEntities();
            var result = (
                from bs in bsList
                where !bs.IsDelete
                join s in sList on bs.ServiceId equals s.Id
                join b in bList on bs.BusinessId equals b.Id
                select new BusinessServiceDto
                {
                    BusinessId = b.Id,
                    Amount = bs.Amount,
                    ServiceId = bs.ServiceId,
                    ServiceName = s.ServiceName
                }
            ).ToListAsync();

            return await result;
        }

    }
}