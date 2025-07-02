namespace EasyTime.Model.Models
{
    public class User : BaseEntity<Guid>
    {
        public int RoleId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsBusinesOwner { get; set; }
        public string? Family { get; set; }
        public int? Age { get; set; }
        public string? ImageName { get; set; }
        public Guid? TokenForChangePassword { get; set; }
        public DateTime? ExpireChangePasswordToken { get; set; }

        #region Relations
        public Role Role { get; set; }
        #endregion
    }
}
