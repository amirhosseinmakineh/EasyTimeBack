namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class RegionDto
    {
        public long Id { get; set; }
        public string RegionName { get; set; }
        public List<NeighborhoodDto> Neighborhoods { get; set; }
    }

    public class RegionDtoDashBoard
    {
        public long Id { get; set; }
        public string RegionName { get; set; }
    }
}
