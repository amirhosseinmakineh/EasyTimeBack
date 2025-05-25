namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class RegionDto
    {
        public long RegionId { get; set; }
        public string RegionName { get; set; }
        public List<NeighborhoodDto> Neighborhoods { get; set; }
    }
}
