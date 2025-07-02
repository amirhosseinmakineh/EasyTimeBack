namespace EasyTime.Model.Models
{
    public class Role : BaseEntity<int>
    {
        public string RoleName { get; set; }

        #region Relations
        public ICollection<User> Users { get; set; }
        #endregion
    }
}
