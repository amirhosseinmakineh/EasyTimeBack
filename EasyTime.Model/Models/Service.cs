using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTime.Model.Models
{
    public class Service : BaseEntity<long>
    {
        public int CategoryId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        #region Relations
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<BusinessServices> BusinessServices { get; set; }
        #endregion
    }
}
