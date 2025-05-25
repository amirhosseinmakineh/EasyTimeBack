namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class BusinessPlaceDto
    {
        public long CityId { get; set; }
        public string CityName { get; set; }
        public List<RegionDto> Regions { get; set; }
        public List<NeighborhoodDto> Neighborhoods { get; set; }
    }
}
