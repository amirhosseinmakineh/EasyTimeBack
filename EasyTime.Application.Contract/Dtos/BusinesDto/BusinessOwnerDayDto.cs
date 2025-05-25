namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class BusinessOwnerDayDto
    {
        public long Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<BusinessOwnerTimeDto> BusinessOwnerTimes { get; set; }
    }
}
