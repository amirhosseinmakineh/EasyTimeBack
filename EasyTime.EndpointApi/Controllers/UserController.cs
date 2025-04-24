using EasyTime.Application.Contract.Dtos;
using EasyTime.Application.Contract.IServices;
using EasyTime.Utilities.Convertor;
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
                return Ok(dto);

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
           await userService.ForgotPassword(dto);
            return Ok();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string password,Guid token)
        {
            var result = await userService.ChangePassword(password,token);
            return Ok(result);
        }
    }
}
