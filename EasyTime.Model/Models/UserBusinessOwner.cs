namespace EasyTime.Model.Models
{
    public class UserBusinessOwner : BaseEntity<long>
    {
        public Guid UserId { get;set;}
        public Guid BusinessOnwerId { get; set; }
        public bool IsExpire { get; set; }
        #region Relations
        public User User { get; set; }
        #endregion
    }
}
