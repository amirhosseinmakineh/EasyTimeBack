namespace EasyTime.Application.Contract.Dtos.BusinessServices
{
    public record BusinessServicesDto
    {
        public long BusinessId { get; set; }
        public long ServiceId { get; set; }
        public float Amount { get; set; }
    }
}
