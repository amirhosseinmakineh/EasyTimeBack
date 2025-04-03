using EasyTime.Application.Contract.Dtos;

namespace EasyTime.Application.Contract.IServices
{
    public interface IUserService
    {
        bool Register(UserDto dto);
        string Login(UserLoginDto dto);
    }
}
