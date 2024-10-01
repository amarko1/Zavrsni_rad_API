using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceModels;
using ServiceLayer.Services.Abstraction;

namespace Zavrsni_rad_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegistrationRequest registerRequest)
        {
            var response = _userService.Register(registerRequest);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var response = _userService.Login(loginRequest);
            return response.IsSuccessful ? Ok(response) : Unauthorized(response);
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshExpiredToken([FromBody] RefreshRequest refreshRequest)
        {
            var response = _userService.RefreshToken(refreshRequest);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Logout")]
        public IActionResult Logout([FromBody] string userEmail)
        {
            var response = _userService.Logout(userEmail);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}
