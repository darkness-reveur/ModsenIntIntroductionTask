using MeetupPlatform.Common.Models.MeetUps;
using MeetupPlatform.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VladosMeetupPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupController : ControllerBase
    {
        private readonly IMeetupService _meetupService;

        public MeetupController(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        [HttpGet]
        [Route("GetAllMeetupsForVisitor")]
        public async Task<IActionResult> GetAllMeetupsForVisitor()
        {
            var meetups = await _meetupService
                .GetAllMeetupsForVisitorAsync();

            if (meetups is not null)
            {
                return Ok(meetups);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetMeetupByIdForOrganiser")]
        [Authorize]
        public async Task<IActionResult> GetMeetupByIdForOrganiser(int id)
        {
            var meetup = await _meetupService
                .GetMeetupByIdForOrganiserAsync(id);

            if (meetup is not null)
            {
                return Ok(meetup);
            }

            return BadRequest(meetup);
        }

        [HttpGet]
        [Route("AddMeetup")]
        [Authorize]
        public async Task<IActionResult> AddMeetup(Meetup meetup)
        {
            if (meetup is not null)
            {
                var addedMeetup = await _meetupService.AddMeetupAsync(meetup);

                return Ok(addedMeetup);
            }

            return BadRequest(null);
        }

        [HttpDelete]
        [Route("DeleteMeetup")]
        [Authorize]
        public async Task<IActionResult> DeleteMeetup(int id)
        {
            var isDeleted = await _meetupService.DeleteMeetupByIdAsync(id);

            if (isDeleted)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _meetupService.GetRoles();

            return Ok(roles);
        }
    }
}
