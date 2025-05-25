namespace EasyTime.Model.Models
{
    public class User : BaseEntity<Guid>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsBusinesOwner { get; set; }
        public string? Famly { get; set; }
        public int? Age { get; set; }
        public string? ImageName { get; set; }
        public Guid? TokenForChangePassword { get; set; }
        public DateTime? ExpireChangePasswordToken { get; set; }
    }
}
