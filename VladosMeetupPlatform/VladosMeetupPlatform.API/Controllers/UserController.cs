using MeetupPlatform.Common.Models.Users;
using MeetupPlatform.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VladosMeetupPlatform.API.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
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
        [Route("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            if(id > 0)
            {
                var user = await _userService.GetUserByIdAsync(id);

                return Ok(user);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if(id > 0)
            {
                var isDeleted = await _userService.DeleteUserAsync(id);

                return Ok(isDeleted);
            }
            return BadRequest();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(User newUser)
        {
            var user = await _userService.UpdateUserAsync(newUser);

            return Ok(user);
        }
    }
}
