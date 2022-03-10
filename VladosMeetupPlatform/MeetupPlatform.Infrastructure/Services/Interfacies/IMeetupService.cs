using MeetupPlatform.Common.Models.MeetUps;

namespace MeetupPlatform.Infrastructure.Services.Interfacies
{
    public interface IMeetupService
    {
        Task<IEnumerable<Meetup>> GetAllMeetupsAsync();

        Task<Meetup> GetMeetupByIdAsync(int id);

        Task<Meetup> AddMeetupAsync(Meetup meetup);

        Task<bool> DeleteMeetupByIdAsync(int id);

        Task<bool> UpdateMeetupAsync(Meetup meetup);

        Task<bool> AddStepAsync(Step step);

        Task<bool> UpdateStepAsync(Step step);

        Task<bool> DeleteStepAsync(int id);

        Task<bool> UpdatePlaceAsync(Place newPlace);
    }
}
