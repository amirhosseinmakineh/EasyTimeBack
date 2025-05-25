namespace EasyTime.Model.Models
{
    public class BusinesRegion:BaseEntity<long>
    {
        public long BusinesCityId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Business> Businesses { get; set; }
        public BusinesCity BusinesCity { get; set; }
    }

}
