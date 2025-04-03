namespace EasyTime.Application.Contract.Dtos
{
    public class UserDto:BaseDto<Guid>
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UserLoginDto : BaseDto<Guid>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
