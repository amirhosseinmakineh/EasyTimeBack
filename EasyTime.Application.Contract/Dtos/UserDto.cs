namespace EasyTime.Application.Contract.Dtos
{
    public class UserDto:BaseDto<Guid>
    {
        public string Email { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public Guid? TokenForChangePassword { get; set; }
        public DateTime? ExpireChangePasswordToken { get; set; }
        public bool? IsBusinessOwner { get; set; }

    }
    public class ForgotPasswordDto:BaseDto<Guid>
    {
        public string Email { get; set; } = string.Empty;
    }
}
