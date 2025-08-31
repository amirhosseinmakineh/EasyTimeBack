using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTime.Model.Models
{
    public class Business : BaseEntity<long>
    {
        public Guid BusinessOwnerId  { get; set; }
        public string BannerName { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public long NeighberhoodId { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("BusinessOwnerId")]
        public User User { get; set; }
        [ForeignKey("CityId")]
        public BusinesCity BusinesCity { get; set; }
        [ForeignKey("RegionId")]
        public BusinesRegion BusinesRegion { get; set; }
        [ForeignKey("NeighberhoodId")]
        public BusinesNeighberhood BusinesNeighberhood { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<BusinessOwnerDay> BusinessOwnerDays { get; set; }
        public ICollection<BusinessOwnerTime> BusinessOwnerTimes { get; set; }
        public ICollection<BusinessDay> BusinessDays { get; set; }
        public ICollection<BusinessServices> BusinessServices { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
