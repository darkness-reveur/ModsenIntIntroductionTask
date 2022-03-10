using MeetupPlatform.Common.Helpers;
using MeetupPlatform.Common.Models.AuthenticationModels;
using MeetupPlatform.Common.Models.Users;
using MeetupPlatform.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatform.Infrastructure.Services.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly MeetupPlatformContext _meetupPlatformContext;
        public AuthService(
            MeetupPlatformContext meetupPlatformContext)
        {
            _meetupPlatformContext = meetupPlatformContext;
        }

        public async Task<bool> IsLoginFree(string login)
        {
            var acessData = await _meetupPlatformContext.AccessData
                .FirstOrDefaultAsync(data => data.Login == login);

            if (acessData == null)
            {
                return true;
            }

            return false;
        }

        public async Task<User> LogIn(string login, string password)
        {
            var salt = await GetUserSaltByLogin(login);

            if(salt is null)
            {
                return null;
            }

            var passwordSalt = Cryptographer.Encrypt(password, salt);

            var accessData = await _meetupPlatformContext.AccessData
                .Include(accessData => accessData.User)
                .FirstOrDefaultAsync(
                data => data.Login == login
                && data.PasswordSalt == passwordSalt);

            var userRole = await _meetupPlatformContext.Roles
                .FirstOrDefaultAsync(role => role.Id == accessData.User.RoleId);

            accessData.User.Role = userRole;

            return accessData.User;
        }

        public async Task Register(string login, string password, User user)
        {
            var encryptedPassword = Cryptographer.Encrypt(password, out byte[] salt);

            var accessData = new AccessDataEntity
            {
                Login = login,
                PasswordSalt = encryptedPassword,
                UserId = user.Id,
                Salt = salt,
            };

            await _meetupPlatformContext.AddAsync(accessData);

            await _meetupPlatformContext.SaveChangesAsync();
        }

        private async Task<byte[]> GetUserSaltByLogin(string login)
        {
            var accessData = await _meetupPlatformContext.AccessData
                .FirstOrDefaultAsync(accessDataEntity => accessDataEntity.Login == login);

            if (accessData is null)
            {
                return null;
            }

            return accessData.Salt;
        }
    }
}
