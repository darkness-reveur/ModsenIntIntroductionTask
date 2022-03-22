using MeetupPlatform.Common.Models.Users;
using MeetupPlatform.Infrastructure.Database;
using MeetupPlatform.Infrastructure.Services.Interfacies;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatform.Infrastructure.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly MeetupPlatformContext _meetupPlatformContext;

        public UserService(
            MeetupPlatformContext meetupPlatformContext)
        {
            _meetupPlatformContext = meetupPlatformContext;
        }

        public async Task<User> AddUserAsync(User newUser)
        {
            if (newUser is not null)
            {
                var exUser = await _meetupPlatformContext.Users
                    .FirstOrDefaultAsync(user => user.Email.Equals(newUser.Email));

                if (exUser is null)
                {
                    await _meetupPlatformContext.Users.AddAsync(newUser);

                    await _meetupPlatformContext.SaveChangesAsync();

                    return newUser;
                }
            }

            return null;
        }

        public async Task<bool> BlockUser(int Id)
        {
            var user = await _meetupPlatformContext.Users
                .FirstOrDefaultAsync(user => user.Id == Id);

            if (user is not null)
            {
                if (!user.IsUserBlocked)
                {
                    user.IsUserBlocked = true;
                }

                return true;
            }

            return false;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            if (userId > 0)
            {
                var user = await _meetupPlatformContext.Users
                   .Include(user => user.Role)
                           .ThenInclude(role => role.Permissions)
                   .FirstOrDefaultAsync(user => user.Id == userId);

                return user;
            }
            return null;            
        }

        public async Task<User> UpdateUserAsync(User newUser, int id)
        {
            if (newUser is null)
            {
                var exUser = await _meetupPlatformContext.Users
                .FirstOrDefaultAsync(user => user.Id == id);

                if (exUser is not null)
                {
                    exUser.Age = newUser.Age;

                    exUser.Email = newUser.Email;

                    exUser.Name = newUser.Name;

                    await _meetupPlatformContext.SaveChangesAsync();

                    return exUser;
                }
            }

            return null;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            if (userId > 0)
            {
                var user = await _meetupPlatformContext.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(user => user.Id == userId);

                if (user is not null)
                {
                    _meetupPlatformContext.Users.Remove(user);

                    await _meetupPlatformContext.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _meetupPlatformContext.Users
                .Include(user => user.Role)
                    .ThenInclude(role => role.Permissions)
                .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roles = await _meetupPlatformContext.Roles
                .Include(role => role.Permissions)
                .ToListAsync();

            return roles;
        }
    }
}
