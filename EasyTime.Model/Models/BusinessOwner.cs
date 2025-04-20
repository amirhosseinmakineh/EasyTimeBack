namespace EasyTime.Model.Models
{
    public class BusinessOwner : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public long NeighberhoodId { get; set; }
        public string Family { get; set; } = string.Empty;
        public int Age { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public BusinesCity BusinesCitiy { get; set; }
        public BusinesRegion BusinesRegion { get; set; }
        public BusinesNeighberhood BusinesNeighberhood { get; set; }
        public ICollection<Business> Busines { get; set; }
        public User User { get; set; }
    }

}
