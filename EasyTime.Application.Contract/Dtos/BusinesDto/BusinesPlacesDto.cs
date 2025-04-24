namespace EasyTime.Application.Contract.Dtos.BusinesDto
{
    public class BusinessDto:BaseDto<long>
    {
        public string BusinessLogo { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string BusinessOwnerName { get; set; } = string.Empty;
    }

    public class BusinessDetailDto
    {
        public long BusinessId { get; set; }
        public Guid BusinesOwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public long NeighberhoodId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public string NeighberhoodName { get; set; } = string.Empty;
        public  List<BusinessOwnerDayDto> BusinessOwnerDayDtos { get; set; }
    }

    public class BusinessPlaceDto
    {
        public string CityName { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public string NeighberhoodName { get; set; } = string.Empty;
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public long NeighberhoodId { get; set; }
    }

    public class BusinessOwnerDayDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<BusinessOwnerTimeDto> BusinessOwnerTimes { get; set; }
    }

    public class BusinessOwnerTimeDto
    {
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public bool IsReserved { get; set; }

    }
    public class ReserveDto : BaseDto<long>
    {
        public Guid UserId { get; set; }
        public long BusinessOwnerDayId { get; set; }
        public long BusinessOwnerTimeId { get; set; }
    }
}
