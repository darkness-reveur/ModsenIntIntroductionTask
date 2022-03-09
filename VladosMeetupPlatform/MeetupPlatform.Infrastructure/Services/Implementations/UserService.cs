using MeetupPlatform.Common.Models.Users;
using MeetupPlatform.Common.Models.ViewModels;
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

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _meetupPlatformContext.Users
                .Include(user => user.Role)
                    .ThenInclude(role => role.RolePermissions)
                        .ThenInclude(rolePerm => rolePerm.Permission)
                .Include(user => user.MeetupVisitors)
                    .ThenInclude(meetupVisitor => meetupVisitor.User)
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

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = await _meetupPlatformContext.Users
                .ToListAsync();

            var viewUsers = new List<UserViewModel>();

            foreach (var user in users)
            {
                var role = await _meetupPlatformContext.Roles
                    .FirstOrDefaultAsync(role => role.Id == user.RoleId);

                var viewUser = new UserViewModel(user);

                var viewRole = await GetViewRole(role);

                viewUser.Role = viewRole;

                viewUsers.Add(viewUser);
            }

            return viewUsers;
        }

        public async Task<IEnumerable<RoleViewModel>> GetRoles()
        {
            var roles = await _meetupPlatformContext.Roles
                .ToListAsync();

            var rolesList = new List<RoleViewModel>();

            foreach (var role in roles)
            {
                var newRole = await GetViewRole(role);

                rolesList.Add(newRole);
            }

            return rolesList;
        }

        private async Task<RoleViewModel> GetViewRole(Role role)
        {
            var permissionsId = await _meetupPlatformContext.RolePermissions
                    .Where(rp => rp.RoleId == role.Id)
                    .Select(perm => perm.PermissionId)
                    .ToListAsync();

            var perms = await _meetupPlatformContext.Permissions
                .Where(perm => permissionsId.Contains(perm.Id))
                .ToListAsync();

            var newRole = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = perms
            };

            return newRole;
        }
    }
}
