namespace EasyTime.Model.Models
{
    public class Rate : BaseEntity<long>
    {
        public float RateNumber { get; set; }
        public Guid UserId { get; set; }
        #region Relations
        public User User { get; set; }
        #endregion

    }

}
