using MeetupPlatform.Common.Models.Users;

namespace MeetupPlatform.Infrastructure.Services.Interfacies
{
    public interface IUserService
    {
        Task<bool> BlockUser(int Id);

        Task<List<User>> GetAllAsync();

        Task<User> GetUserByIdAsync(int userId);

        Task<User> AddUserAsync(User user);

        Task<User> UpdateUserAsync(User user);
    }
}
