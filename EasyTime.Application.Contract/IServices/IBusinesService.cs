using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Utilities.Convertor;

namespace EasyTime.Application.Contract.IServices
{
    public interface IBusinesService
    {
        Task<BusinessPlaceDto> FilterBusinesByPlace(long? businesCityId,long? regionId);
        Task<IQueryable<BusinessDto>> FilterBusines(long businesCityId);
        Task<BusinessDetailDto> GetBusinessDetail(long businessId);
        Task<Result<ReserveDto>> Reserve(ReserveDto dto);
        Task<Result<bool>> ValidateReserve(ReserveDto dto);
        Task<List<CityDto>> GetAllCitiesAsync();
    }
    public interface IService
    {

    }
}
