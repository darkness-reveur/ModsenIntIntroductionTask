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
                    await _meetupPlatformContext.AddAsync(newUser);

                    await _meetupPlatformContext.SaveChangesAsync();

                    return newUser;
                }
                return null;
            }
            else
            {
                return null;
            }
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

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _meetupPlatformContext.Users
                .Include(user => user.Role)
                    .ThenInclude(role => role.Permissions)
                .ToListAsync();

            return users;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _meetupPlatformContext.Users
                .Include(user => user.Role)
                    .ThenInclude(role => role.Permissions)
                .Include(user => user.Meetups)
                .FirstOrDefaultAsync(user => user.Id == userId);

            return user;
        }

        public async Task<User> UpdateUserAsync(User newUser)
        {
            var exUser = await _meetupPlatformContext.Users
                .FirstOrDefaultAsync(user => user.Id == newUser.Id);

            if (exUser is not null)
            {
                exUser.Age = newUser.Age;

                exUser.Email = newUser.Email;

                exUser.Name = newUser.Name;

                _meetupPlatformContext.Users.Update(exUser);

                await _meetupPlatformContext.SaveChangesAsync();

                return exUser;
            }
            return null;
        }
    }
}
