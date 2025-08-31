using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTime.Model.Models
{
    public class Achievements:BaseEntity<long>
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion
    }
}
