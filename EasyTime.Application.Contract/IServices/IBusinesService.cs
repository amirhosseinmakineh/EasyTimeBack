using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Application.Contract.Dtos.BusinessServices;
using EasyTime.Application.Contract.Dtos.Category;
using EasyTime.Application.Contract.Dtos.Service;
using EasyTime.Utilities.Convertor;
using System.Threading.Tasks;

namespace EasyTime.Application.Contract.IServices
{
    public interface IBusinesService
    {
        Task<BusinessPlaceDto> FilterBusinesByPlace(long? businesCityId,long? regionId);
        Task<List<BusinessDto>> FilterBusinesses(long neighborhoodId, List<long>? serviceIdes,int? categoryId, int skip = 0, int take = 6, decimal? maxAmount = 0);
        Task<BusinessDetailDto> GetBusinessDetail(long businessId);
        Task<Result<ReserveDto>> Reserve(ReserveDto dto);
        Task<Result<bool>> ValidateReserve(ReserveDto dto);
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<List<CategoryDto>> GetAllCategories();
        Task<List<ServiceDto>> GetServicseWithCategory(int categoryId);
        Task<decimal> GetBusinessServicesAmount();
        Task<SearachDto> GetSelectedPlace(long cityId , long regionId , long neighberHoodId );
        Task<List<BusinessServiceDto>> GetAllServices();
    }
    public interface IService
    {

    }
}
