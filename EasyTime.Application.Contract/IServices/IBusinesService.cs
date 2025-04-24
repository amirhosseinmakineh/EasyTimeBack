using EasyTime.Application.Contract.Dtos.BusinesDto;
using EasyTime.Utilities.Convertor;

namespace EasyTime.Application.Contract.IServices
{
    public interface IBusinesService
    {
        Task<IQueryable<BusinessPlaceDto>> FilterBusinesByPlace(long businesCityId);
        Task<IQueryable<BusinessDto>> FilterBusines(long businesCityId);
        Task<BusinessDetailDto> GetBusinessDetail(long businessId);
        Task<Result<ReserveDto>> Reserve(ReserveDto dto);
        Task<Result<bool>> ValidateReserve(ReserveDto dto);

    }
}
