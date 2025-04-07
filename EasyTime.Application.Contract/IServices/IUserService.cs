using EasyTime.Application.Contract.Dtos;
using EasyTime.Utilities.Convertor;

namespace EasyTime.Application.Contract.IServices
{
    public interface IUserService
    {
        bool Register(UserDto dto);
        Result<string> Login(UserLoginDto dto);
        Result<bool> ForgotPassword(UserDto dto);
        Result<string> ChangePassword(string newPassword, Guid expireToken);
    }
}
