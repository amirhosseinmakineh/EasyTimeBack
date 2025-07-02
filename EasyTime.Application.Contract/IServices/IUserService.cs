using EasyTime.Application.Contract.Dtos;
using EasyTime.Application.Contract.Dtos.BusinessOwnerDtos;
using EasyTime.Utilities.Convertor;

namespace EasyTime.Application.Contract.IServices
{
    public interface IUserService
    {
        Task<bool> Register(UserDto dto);
        Task<Result<string>> Login(UserLoginDto dto);
        Task<Result<bool>> ForgotPassword(UserDto dto);
        Task<Result<string>> ChangePassword(string newPassword, Guid expireToken);
        Task<Result<string>> CheckResetToken(Guid TokenForChangePassword);
        Task<Result<object>> UpdateBusinessOwnerInfo(UpdateBusinessOwnerInfoDto dto);
    }
}
