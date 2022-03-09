using MeetupPlatform.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupPlatform.Infrastructure.Services.AuthenticationService
{
    public interface IAuthService
    {
        Task Register(
           string login,
           string password,
           User user);

        Task<bool> IsLoginFree(string login);

        Task<User> LogIn(
            string login,
            string password);
    }
}
