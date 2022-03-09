using MeetupPlatform.Common.Models.Users;
using MeetupPlatform.Common.Models.ViewModels;

namespace MeetupPlatform.Infrastructure.Services.Interfacies
{
    public interface IUserService
    {
        Task<bool> BlockUser(int Id);

        Task<IEnumerable<UserViewModel>> GetAllAsync();

        Task<User> GetUserByIdAsync(int userId);

        Task<User> AddUserAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task<IEnumerable<RoleViewModel>> GetRoles();
    }
}
