namespace EasyTime.Model.Models
{
    public class BusinesNeighberhood:BaseEntity<long>
    {
        public long BusinesRegionId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Business> Businesses { get; set; }
        public BusinesRegion BusinesRegion { get; set; }
    }

}
