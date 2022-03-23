using MeetupPlatform.Common.Models.Users;
using MeetupPlatform.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VladosMeetupPlatform.API.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets all of users
        /// </summary>
        /// <returns>List all of users</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        /// <summary>
        /// Gets the user using specified id
        /// </summary>
        /// <param name="id" example="123"></param>
        /// <returns>User with entered id</returns>
        [HttpGet("{id}/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id > 0)
            {
                var user = await _userService.GetUserByIdAsync(id);

                return user is not null ? Ok(user) : NotFound();
            }
            return BadRequest();
        }

        /// <summary>
        /// Deletes the user using specified id
        /// </summary>
        /// <param name="id" example="123"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id > 0)
            {
                var isDeleted = await _userService.DeleteUserAsync(id);

                return isDeleted ? NoContent() : NotFound();
            }
            return BadRequest();
        }

        /// <summary>
        /// Updates user with specified id
        /// </summary>
        /// <param name="id" example="123"></param>
        /// <param name="newUser"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody]User newUser)
        {
            if(id > 0)
            {
                var user = await _userService.UpdateUserAsync(newUser, id);

                return user is not null ? NoContent() : NotFound();
            }
            return BadRequest();
        }
    }
}
