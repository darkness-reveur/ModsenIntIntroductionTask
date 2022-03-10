using MeetupPlatform.Common.Models.AuthenticationModels;
using MeetupPlatform.Common.Models.Users;

namespace MeetupPlatform.Infrastructure.Services.AuthenticationService
{
    public interface IAuthService
    {
        Task Register(RegisterData registerData);

        Task<bool> IsLoginFree(string login);

        Task<User> LogIn(LoginData loginData);
    }
}
