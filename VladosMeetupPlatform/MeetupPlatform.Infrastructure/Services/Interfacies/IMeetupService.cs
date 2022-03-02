using MeetupPlatform.Common.Models.MeetUps;
using MeetupPlatform.Common.Models.Users;

namespace MeetupPlatform.Infrastructure.Services.Interfacies
{
    public interface IMeetupService
    {
        Task<IEnumerable<Meetup>> GetAllMeetupsForVisitorAsync();

        Task<Meetup> GetMeetupByIdForVisitorAsync(int id);

        Task<Meetup> GetMeetupByIdForOrganiserAsync(int id);

        Task<Meetup> AddMeetupAsync(Meetup meetup);

        Task<bool> DeleteMeetupByIdAsync(int id);

        Task<bool> UpdateMeetupAsync(Meetup meetup);

        Task<bool> AddStepAsync(Step step);

        Task<bool> UpdateStepAsync(Step step);

        Task<bool> DeleteStepAsync(int id);

        Task<bool> UpdatePlaceAsync(Place newPlace);

        Task<IEnumerable<Role>> GetRoles();
    }
}
