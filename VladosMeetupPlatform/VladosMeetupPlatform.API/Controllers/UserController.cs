using MeetupPlatform.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VladosMeetupPlatform.API.Controllers
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

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService
                .GetAllAsync();

            if(users is not null)
            {
                return Ok(users);
            }

            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            if(true)
            {
                var user = _userService.GetUserByIdAsync(1);

                return Ok(user);
            }
            return BadRequest();
        }
    }
}
