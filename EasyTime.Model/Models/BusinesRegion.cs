namespace EasyTime.Model.Models
{
    public class BusinesRegion:BaseEntity<long>
    {
        public Guid BusinesOwnerId { get; set; }
        public long BusinesCityId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<BusinessOwner> BusinesOwners { get; set; }
        public BusinesCity BusinesCity { get; set; }
        public ICollection<BusinesNeighberhood> BusinesNeighberhoods { get; set; }
    }

}
