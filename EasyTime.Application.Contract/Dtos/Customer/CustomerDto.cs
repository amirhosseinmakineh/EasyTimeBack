using EasyTime.Application.Contract.Enums;

namespace EasyTime.Application.Contract.Dtos.Customer
{
    public record CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public long DayId { get; set; }
        public long TimeId { get; set; }
        public string DayName { get; set; } = string.Empty;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }

}
