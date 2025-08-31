namespace EasyTime.Application.Contract.Dtos.Service
{
    public record SearachDto
    {
        public long CityId  { get; set; }
        public long RegionId { get; set; }
        public long NeighberHoodId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public string NeighberHoodName { get; set; } = string.Empty;
    }
}
