namespace EasyTime.Model.Models
{
    public class BusinesCity : BaseEntity<long>
    {
        public Guid BusinesOwnerId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<BusinessOwner> BusinesOwners { get; set; }
        public ICollection<BusinesRegion> BusinesRegions { get; set; }
    }

}
