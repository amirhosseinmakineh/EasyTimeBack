using EasyTime.Application.Contract.Dtos;
using EasyTime.Application.Contract.IServices;
using EasyTime.Utilities.Convertor;
using Microsoft.AspNetCore.Http;
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
            {
                return Ok(dto);
            }
           var responce =  Result<UserDto>.Failure(new Error(StatusCodes.Status409Conflict.ToString(), "User Already Exist"));
            return Ok(responce);

        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginDto dto)
        {
            userService.Login(dto);
            return Ok(dto);
        }
    }
}
