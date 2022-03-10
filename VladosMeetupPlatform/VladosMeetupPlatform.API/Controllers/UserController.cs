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
        public async Task<IActionResult> GetUser(int id)
        {
            if(id > 0)
            {
                var user = _userService.GetUserByIdAsync(id);

                return Ok(user);
            }
            return BadRequest();
        }

        [HttpDelete]
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
        public async Task<IActionResult> UpdateUser(User newUser)
        {
            var user = _userService.UpdateUserAsync(newUser);

            return Ok(user);
        }
    }
}
