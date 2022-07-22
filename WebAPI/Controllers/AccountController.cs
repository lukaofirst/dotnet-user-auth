using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IJWTManagerService _jwtManagerService;
        private readonly IUserService _userService;

        public AccountController(IJWTManagerService jwtManagerService, IUserService userService)
        {
            _jwtManagerService = jwtManagerService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll();

            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(User user)
        {
            var token = await _jwtManagerService.Authenticate(user);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user)
        {
            var emailExist = await _userService.FindByEmail(user.email!);

            if(emailExist != null) return BadRequest("Email already exist!");

            var entity = await _userService.InsertOne(user);

            return Ok(entity);
        }
    }
}
