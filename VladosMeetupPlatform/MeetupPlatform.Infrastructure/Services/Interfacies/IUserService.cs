using MeetupPlatform.Common.Models.Users;

namespace MeetupPlatform.Infrastructure.Services.Interfacies
{
    public interface IUserService
    {
        Task<bool> BlockUser(int Id);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetUserByIdAsync(int userId);

        Task<User> AddUserAsync(User user);

        Task<User> UpdateUserAsync(User user, int id);

        Task<IEnumerable<Role>> GetRoles();

        Task<bool> DeleteUserAsync(int userId);
    }
}
