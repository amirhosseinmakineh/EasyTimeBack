using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTime.Model.Models
{
    public class BusinessServices : BaseEntity<long>
    {

        public long BusinessId { get; set; }
        public long  ServiceId { get; set; }
        public float Amount  { get; set; }

        #region Relations
        [ForeignKey("BusinessId")]
        public Business Business { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        #endregion
    }
}
