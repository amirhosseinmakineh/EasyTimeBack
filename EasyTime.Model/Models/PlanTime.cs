namespace EasyTime.Model.Models
{
    public class PlanTime : BaseEntity<long>
    {
        public long PlanId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string PlanName { get; set; }
        public float AmountAdded { get; set; }  


        #region Relation
        public ICollection<Plan> Plans { get; set; }
        public ICollection<BusinessOwnerPlan> BusinessOwnerPlans { get; set; }
        #endregion
    }
}
