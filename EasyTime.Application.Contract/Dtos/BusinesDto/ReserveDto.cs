namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class ReserveDto : BaseDto<long>
    {
        public Guid UserId { get; set; }
        public long BusinessOwnerDayId { get; set; }
        public long BusinessOwnerTimeId { get; set; }
    }
}
