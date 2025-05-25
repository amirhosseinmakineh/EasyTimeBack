namespace EasyTime.Model.Models
{
    public class BusinesCity : BaseEntity<long>
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<BusinesRegion> BusinesRegions { get; set; }
        public ICollection<Business> Businesses { get; set; }
    }

}
