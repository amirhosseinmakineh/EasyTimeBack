using EasyTime.Application.Contract.Dtos.BusinesDto;

namespace EasyTime.Application.Contract.Dtos.BusinessOwnerDtos
{
    public class UpdateBusinessOwnerInfoDto : BaseDto<Guid>
    {
        public string ImageName { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public int Age { get; set; }
        public CityDto City { get; set; }
        public RegionDtoDashBoard Region { get; set; }
        public NeighborhoodDto Neighborhood { get; set; }
        public BusinessDto Business { get; set; }
        public List<long> DayIdes { get; set; }
        public List<long> TimeIdes { get; set; }
    }
}
