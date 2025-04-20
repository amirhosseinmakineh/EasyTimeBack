namespace EasyTime.Model.Models
{
    public class Business : BaseEntity<long>
    {
        public Guid BusinesOwnerId  { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public long NeighberhoodId { get; set; }
        public BusinessOwner BusinessOwner { get; set; }
        public BusinesCity BusinesCity { get; set; }
        public BusinesRegion BusinesRegion { get; set; }
        public BusinesNeighberhood BusinesNeighberhood { get; set; }
        public ICollection<BusinessOwnerDay> BusinessOwnerDays { get; set; }
        public ICollection<BusinessOwnerTime> BusinessOwnerTimes { get; set; }

    }

}
