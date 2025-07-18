using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTime.Model.Models
{
    public class BusinessDay : BaseEntity<long>
    {
        public long BusinessId { get; set; }
        public long DayId { get; set; }

        #region Relations
        public Business Business { get; set; }
        [ForeignKey("DayId")]
        public BusinessOwnerDay BusinessOwnerDay { get; set; }
        public ICollection<BusinessTime> BusinessTimes { get; set; }
        #endregion
    }
}
