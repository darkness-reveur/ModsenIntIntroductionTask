﻿using MeetupPlatform.Common.Models.MeetUps;
using MeetupPlatform.Infrastructure.Database;
using MeetupPlatform.Infrastructure.Services.Interfacies;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatform.Infrastructure.Services.Implementations
{
    public class MeetupService : IMeetupService
    {
        private readonly MeetupPlatformContext _meetupPlatformContext;

        public MeetupService(MeetupPlatformContext meetupPlatformContext)
        {
            _meetupPlatformContext = meetupPlatformContext;
        }

        public async Task<Meetup> AddMeetupAsync(Meetup meetup)
        {
            if (meetup is not null
                && meetup.Id == 0)
            {
                await _meetupPlatformContext.Meetups.AddAsync(meetup);

                var org = await _meetupPlatformContext.Users
                    .FirstOrDefaultAsync(user => user.Id == meetup.UserOrganizerId);

                await _meetupPlatformContext.SaveChangesAsync();
            }

            return meetup;
        }

        public async Task<bool> DeleteMeetupByIdAsync(int id)
        {
            if (id > 0)
            {
                var exMeetup = await _meetupPlatformContext.Meetups
                    .AsNoTracking()
                    .FirstOrDefaultAsync(meetup => meetup.Id == id);

                if (exMeetup is not null)
                {
                    _meetupPlatformContext.Meetups.Remove(exMeetup);

                    await _meetupPlatformContext.SaveChangesAsync();

                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Meetup>> GetAllMeetupsAsync()
        {
            var meetups = await _meetupPlatformContext.Meetups
                .Include(meetup => meetup.Place)
                .Include(meetup => meetup.Steps)
                    .ThenInclude(step => step.UserSpeaker)
                .ToListAsync();

            return meetups;
        }

        public async Task<Meetup> GetMeetupByIdAsync(int id)
        {
            var meetup = await _meetupPlatformContext.Meetups
                .Include(meetup => meetup.Place)
                .Include(meetup => meetup.Steps)
                    .ThenInclude(step => step.UserSpeaker)
                .FirstOrDefaultAsync(meetup => meetup.Id == id);

            return meetup;
        }

        public async Task<bool> UpdateMeetupAsync(Meetup newMeetup)
        {
            if (newMeetup is not null)
            {
                var exMeetup = await _meetupPlatformContext.Meetups
                    .FirstOrDefaultAsync(meetup => meetup.Id == newMeetup.Id);

                exMeetup.Name = newMeetup.Name;

                exMeetup.Description = newMeetup.Description;

                exMeetup.StartTime = newMeetup.StartTime;

                exMeetup.EndTime = newMeetup.EndTime;

                exMeetup.IsMeetForAuthorizedUsers = newMeetup.IsMeetForAuthorizedUsers;

                exMeetup.CountOfVisitors = newMeetup.CountOfVisitors;

                exMeetup.Place = newMeetup.Place;

                exMeetup.Steps = newMeetup.Steps;

                _meetupPlatformContext.Meetups.Update(exMeetup);

                await _meetupPlatformContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> AddStepAsync(Step step)
        {
            if (step is not null)
            {
                await _meetupPlatformContext.Steps.AddAsync(step);

                await _meetupPlatformContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> DeleteStepAsync(int id)
        {
            if (id > 0)
            {
                var exStep = await _meetupPlatformContext.Steps
                    .FirstOrDefaultAsync(step => step.Id == id);

                if (exStep is not null)
                {
                    _meetupPlatformContext.Steps.Remove(exStep);

                    await _meetupPlatformContext.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateStepAsync(Step newStep)
        {
            if (newStep is not null)
            {
                var exStep = await _meetupPlatformContext.Steps
                    .FirstOrDefaultAsync(step => step.Id == newStep.Id);

                if (exStep is not null)
                {
                    exStep.Description = newStep.Description;

                    exStep.Name = newStep.Name;

                    exStep.StartTime = newStep.StartTime;

                    exStep.EndTime = newStep.EndTime;

                    exStep.UserSpeakerId = newStep.UserSpeakerId;

                    _meetupPlatformContext.Steps.Update(exStep);

                    await _meetupPlatformContext.SaveChangesAsync();

                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdatePlaceAsync(Place newPlace)
        {
            if (newPlace is not null)
            {
                var exPlace = await _meetupPlatformContext.Places
                    .FirstOrDefaultAsync(place => place.Id == newPlace.Id);

                if (exPlace is not null)
                {
                    exPlace.Description = newPlace.Description;

                    exPlace.Name = newPlace.Name;

                    exPlace.LinkToGoogleMap = newPlace.LinkToGoogleMap;

                    exPlace.CountOfPlaces = newPlace.CountOfPlaces;

                    return true;
                }
            }
            return false;
        }
    }
}
