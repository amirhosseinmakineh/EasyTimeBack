using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTime.Model.Models
{
    public class PlansInformation: BaseEntity<long>
    {
        public long PlanId { get; set; }
        public string PlanService { get; set; } = string.Empty;

        #region Relation
        [ForeignKey("PlanId")]
        public Plan Plan { get; set; }
        #endregion
    }
}
