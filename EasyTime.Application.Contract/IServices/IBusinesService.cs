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
        Task<List<BusinessDto>> FilterBusinesses(long neighborhoodId, int skip = 0, int take = 6, float? maxAmount = 0, int? categoryId = 0);
        Task<BusinessDetailDto> GetBusinessDetail(long businessId);
        Task<Result<ReserveDto>> Reserve(ReserveDto dto);
        Task<Result<bool>> ValidateReserve(ReserveDto dto);
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<List<CategoryDto>> GetAllCategories();
        Task<List<ServiceDto>> GetServicseWithCategory(int categoryId);
        Task<float> GetBusinessServicesAmount();
    }
    public interface IService
    {

    }
}
