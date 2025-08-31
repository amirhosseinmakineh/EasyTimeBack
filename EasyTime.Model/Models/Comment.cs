namespace EasyTime.Model.Models
{
    public class Comment : BaseEntity<long>
    {
        public Guid UserId { get; set; }
        public long BusinessId { get; set; }
        public string CommentText { get; set; }
        public bool IsLocked { get; set; }
        #region Relations
        public User User { get; set; }
        public Business Business { get; set; }
        #endregion
    }
}
