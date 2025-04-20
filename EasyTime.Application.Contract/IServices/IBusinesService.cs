using EasyTime.Application.Contract.Dtos.BusinesDto;

namespace EasyTime.Application.Contract.IServices
{
    public interface IBusinesService
    {
        IQueryable<BusinessPlaceDto> FilterBusinesByPlace(long businesCityId);
        IQueryable<BusinessDto> FilterBusines(long businesCityId);
        BusinessDetailDto GetBusinessDetail(long businessId);

    }
}
