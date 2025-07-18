using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTime.Model.Models
{
    public class BusinessTime : BaseEntity<long>
    {
        public long BusinessDayId { get; set; }
        public long TimeId { get; set; }
        #region Relations
        [ForeignKey("BusinessDayId")]
        public BusinessDay BusinessDay { get; set; }
        [ForeignKey("TimeId")]
        public BusinessOwnerTime BusinessOwnerTime { get; set; }
        #endregion
    }
}
