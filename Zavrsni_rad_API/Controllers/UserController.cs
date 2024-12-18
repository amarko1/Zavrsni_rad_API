using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
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

        [HttpGet("GetUser/{userId}")]
        public IActionResult GetUser(int userId)
        {
            UserDto user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound("User by that id not found");
            }

            return Ok(user);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("DisableUser")]
        public IActionResult DisableUser([FromBody] int id)
        {
            _userService.DisableUser(id);
            return Ok(new { Message = "User disabled successfully" });
        }

        [HttpPost("EnableUser")]
        public IActionResult EnableUser([FromBody] int id)
        {
            _userService.EnableUser(id);
            return Ok(new { Message = "User enabled successfully" });
        }

        [HttpPost("UpdateUserRole")]
        public IActionResult UpdateUserRole([FromBody] UpdateUserRoleRequest request)
        {
            try
            {
                _userService.UpdateUserRole(request.UserId, request.NewRole);
                return Ok(new { Message = "User role updated successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
