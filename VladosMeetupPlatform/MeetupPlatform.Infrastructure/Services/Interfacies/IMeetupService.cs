using MeetupPlatform.Common.Models.MeetUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupPlatform.Infrastructure.Services.Interfacies
{
    public interface IMeetupService
    {
        Task<IEnumerable<Meetup>> GetAllMeetupsForVisitorAsync();

        Task<Meetup> GetMeetupByIdForVisitorAsync(int id);

        Task<Meetup> GetMeetupByIdForOrganiser(int id);

        Task<bool> AddMeetupAsync(Meetup meetup);

        Task<bool> DeleteMeetupByIdAsync(int id);

        Task<bool> UpdateMeetupAsync(Meetup meetup);

        Task<bool> AddStepAsync(Step step);

        Task<bool> UpdateStepAsync(Step step);

        Task<bool> DeleteStepAsync(int id);

        Task<bool> UpdatePlaceAsync(Place newPlace);
    }
}
