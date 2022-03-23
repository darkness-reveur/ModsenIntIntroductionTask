using MeetupPlatform.Common.Models.MeetUps;
using MeetupPlatform.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VladosMeetupPlatform.API.Controllers
{
    /// <summary>
    /// Main controller
    /// </summary>
    [Route("api/meetups/")]
    [ApiController]
    public class MeetupController : ControllerBase
    {
        private readonly IMeetupService _meetupService;

        public MeetupController(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        /// <summary>
        /// Gets all of meetups
        /// </summary>
        /// <returns>List of all meetups</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Meetup>))]
        public async Task<IActionResult> GetAllMeetups()
        {
            var meetups = await _meetupService.GetAllMeetupsAsync();

            return Ok(meetups);
        }

        /// <summary>
        /// Gets the meetup using specified id
        /// </summary>
        /// <param name="id" example="123"></param>
        /// <returns>Meetup with entered id</returns>
        [HttpGet("{id}/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Meetup))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMeetupById(int id)
        {
            if (id > 0)
            {
                var meetup = await _meetupService.GetMeetupByIdAsync(id);

                return meetup is not null ? Ok(meetup) : NotFound();
            }

            return BadRequest("Incorrect Id");
        }
        /// <summary>
        /// Adds new meetup
        /// </summary>
        /// <param name="meetup"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> AddMeetup(Meetup meetup)
        {
            if (meetup is not null)
            {
                var addedMeetup = await _meetupService.AddMeetupAsync(meetup);

                return CreatedAtAction(nameof(GetMeetupById), new { id = addedMeetup.Id }, addedMeetup);
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates the meetup using specified id
        /// </summary>
        /// <param name="id" example="123"></param>
        /// <param name="meetup"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> UpdateMeetup(int id, [FromBody] Meetup meetup)
        {
            if (meetup is not null && id > 0)
            {
                var isUpdated = await _meetupService.UpdateMeetupAsync(meetup, id);

                return isUpdated ? NoContent() : NotFound();
            }

            return BadRequest();
        }

        /// <summary>
        /// Deletes the meetup using specified id
        /// </summary>
        /// <param name="id" example="123"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> DeleteMeetup(int id)
        {
            if (id > 0)
            {
                var isDeleted = await _meetupService.DeleteMeetupByIdAsync(id);

                return isDeleted ? NoContent() : NotFound();
            }

            return BadRequest();
        }

        /// <summary>
        /// Adds new step in meetup with entered id
        /// </summary>
        /// <param name="meetupId" example="123"></param>
        /// <param name="step"></param>
        [HttpPost("{meetupId}/steps/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "editor")]
        public async Task<IActionResult> AddStepToMeetup(int meetupId, [FromBody] Step step)
        {
            if (step is not null && meetupId > 0)
            {
                var meetup = await _meetupService.GetMeetupByIdAsync(meetupId);

                if (meetup is null)
                {
                    return BadRequest();
                }

                var addedMeetup = _meetupService.AddStepAsync(step);

                if (addedMeetup is not null)
                {
                    return CreatedAtAction(nameof(GetMeetupById), new { id = addedMeetup.Id }, addedMeetup);
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Deletes the step in meetup with specified id, using specified id
        /// </summary>
        /// <param name="meetupId" example="123"></param>
        /// <param name="stepId" example="123"></param>
        [HttpDelete("{meetupId}/steps/{stepId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStep(int meetupId, int stepId)
        {
            if (stepId > 0 && meetupId > 0)
            {
                var meetup = await _meetupService.GetMeetupByIdAsync(meetupId);

                if (meetup is null)
                {
                    return NotFound();
                }

                var isDeleted = await _meetupService.DeleteStepAsync(stepId);

                return isDeleted ? NoContent() : NotFound();
            }

            return BadRequest();
        }
    }
}
