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
        public IActionResult Register(UserDto dto)
        {
            if (userService.Register(dto))
                return Ok(dto);

            var responce = Result<UserDto>.Failure("User Already Exist");
            return Ok(responce);

        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginDto dto)
        {
            var result = userService.Login(dto);
            if (result.IsSuccess)
                return Ok(result);
            return Ok(result);
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(UserDto dto)
        {
            userService.ForgotPassword(dto);
            return Ok();
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(string password,Guid token)
        {
            var result = userService.ChangePassword(password,token);
            return Ok(result);
        }
    }
}
