namespace EasyTime.Application.Contract.Dtos.Service
{
    public record BusinessServiceDto
    {
        public long BusinessId { get; set; }
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Amount { get; set; }
    }

}
