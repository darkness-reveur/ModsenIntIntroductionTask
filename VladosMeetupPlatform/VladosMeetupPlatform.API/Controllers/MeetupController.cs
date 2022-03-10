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
        [Route("GetAllMeetups")]
        public async Task<IActionResult> GetAllMeetups()
        {
            var meetups = await _meetupService
                .GetAllMeetupsAsync();

            if (meetups is not null)
            {
                return Ok(meetups);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetMeetupById")]
        [Authorize]
        public async Task<IActionResult> GetMeetupById(int id)
        {
            var meetup = await _meetupService
                .GetMeetupByIdAsync(id);

            if (meetup is not null)
            {
                return Ok(meetup);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("AddMeetup")]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> AddMeetup(Meetup meetup)
        {
            if (meetup is not null)
            {
                var addedMeetup = await _meetupService.AddMeetupAsync(meetup);

                return Ok(addedMeetup);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateMeetup")]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> UpdateMeetup(Meetup meetup)
        {
            if (meetup is not null)
            {
                var updatedMeetup = await _meetupService.UpdateMeetupAsync(meetup);

                return Ok(updatedMeetup);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteMeetup")]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> DeleteMeetup(int id)
        {
            var isDeleted = await _meetupService.DeleteMeetupByIdAsync(id);

            if (isDeleted)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("AddStep")]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> AddStepToMeetup(Step step)
        {
            if(step is not null)
            {
                await _meetupService.AddStepAsync(step);

                return Ok(step);
            }
            return BadRequest(false);
        }

        [HttpDelete]
        [Route("DeleteStep")]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> DeleteStep(int stepId)
        {
            if (stepId > 0)
            {
                await _meetupService.DeleteStepAsync(stepId);

                return Ok();
            }
            return BadRequest();
        }
    }
}
