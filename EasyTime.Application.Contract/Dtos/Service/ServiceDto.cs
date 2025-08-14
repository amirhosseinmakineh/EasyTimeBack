namespace EasyTime.Application.Contract.Dtos.Service
{
    public record ServiceDto
    {
        public string ServiceName { get; set; } = string.Empty;
        public long ServiceId { get; set; }
        public int CategoryId { get; set; }
    }
}
