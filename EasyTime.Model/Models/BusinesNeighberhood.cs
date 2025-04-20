namespace EasyTime.Model.Models
{
    public class BusinesNeighberhood:BaseEntity<long>
    {
        public Guid BusinesOwnerId { get; set; }
        public long BusinesRegionId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<BusinessOwner> BusinesOwners { get; set; }
        public BusinesRegion BusinesRegion { get; set; }
    }

}
