namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public record BusinessOwnerProfileDto
    {
        public Guid Id  { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
    }
}
