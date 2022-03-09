using MeetupPlatform.Infrastructure.Services.AuthenticationService;
using MeetupPlatform.Infrastructure.Services.Implementations;
using MeetupPlatform.Infrastructure.Services.Interfacies;

namespace VladosMeetupPlatform.API
{
    public static class DependencyContainer
    {
        public static void RegisterDependesy(this IServiceCollection service)
        {
            service.AddTransient<IMeetupService, MeetupService>();

            service.AddTransient<IUserService, UserService>();

            service.AddTransient<IAuthService, AuthService>();
        }
    }
}
