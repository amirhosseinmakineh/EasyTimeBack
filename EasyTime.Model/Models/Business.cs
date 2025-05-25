using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTime.Model.Models
{
    public class Business : BaseEntity<long>
    {
        public Guid UserId  { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public long NeighberhoodId { get; set; }
        [ForeignKey("BusinesOwnerId")]
        public User User { get; set; }
        [ForeignKey("CityId")]
        public BusinesCity BusinesCity { get; set; }
        [ForeignKey("RegionId")]
        public BusinesRegion BusinesRegion { get; set; }
        [ForeignKey("NeighberhoodId")]
        public BusinesNeighberhood BusinesNeighberhood { get; set; }
        public ICollection<BusinessOwnerDay> BusinessOwnerDays { get; set; }
        public ICollection<BusinessOwnerTime> BusinessOwnerTimes { get; set; }

    }

}
