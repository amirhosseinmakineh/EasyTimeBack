namespace EasyTime.Model.Models
{
    public class BusinessOwnerPlan : BaseEntity<long>
    {
        public Guid UserId { get; set; }
        public long PlanId { get; set; }
        public bool IsExpire { get; set; }
        public long PlanTimeId { get; set; }

        #region Relations 
        public User User { get; set; }
        public Plan Plan { get; set; }
        public PlanTime PlanTime { get; set; }
        #endregion
    }
}
