using EasyTime.Application.Attributes;
using EasyTime.Application.Contract.Dtos;
using EasyTime.Application.Contract.Dtos.BusinessOwnerDtos;
using EasyTime.Application.Contract.IServices;
using EasyTime.Utilities.Convertor;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EasyTime.EndpointApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto dto)
        {
            if (await userService.Register(dto))
            {
                var response = Result<UserDto>.Success(dto, "ثبت نام با موفقیت انجام شد");
                return Ok(response);
            }

            var responce = Result<UserDto>.Failure("User Already Exist");
            return Ok(responce);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var result = await userService.Login(dto);
            if (result.IsSuccess)
                return Ok(result);
            return Ok(result);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(UserDto dto)
        {
           var result = await userService.ForgotPassword(dto);
            return Ok(result);
        }

        [HttpGet("CheckChangePasswordToken/{TokenForChangePassword}")]
        public async Task<IActionResult> CheckChangePasswordToken(Guid TokenForChangePassword)
        {
            var result = await userService.CheckResetToken(TokenForChangePassword);
            return Ok(result);
        }

        [HttpPost("ChangePassword/{TokenForChangePassword}")]
        public async Task<IActionResult> ChangePassword(string password,Guid TokenForChangePassword)
        {
            var result = await userService.ChangePassword(password, TokenForChangePassword);
            return Ok(result);
        }

        [HttpPost("DashBoard")]
        [Rolemanager("BusinessOwner")]
        public IActionResult CreateBusiness(UpdateBusinessOwnerInfoDto dto)
        {
            var resutl = userService.UpdateBusinessOwnerInfo(dto);
            return Ok(resutl);
        }

        [HttpGet("BusinessOwnerProfile")]
        public async Task<IActionResult> GetBusinessOwnerProfile(Guid id)
        {
             var result = await userService.GetBusinessOwnerProfile(id);
            return Ok(result);
        }
    }
}
