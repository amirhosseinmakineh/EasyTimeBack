namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class BusinessOwnerTimeDto
    {
        public long Id { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public bool IsReserved { get; set; }

    }
}
