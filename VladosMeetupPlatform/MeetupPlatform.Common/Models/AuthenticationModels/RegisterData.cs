using MeetupPlatform.Common.Models.Users;

namespace MeetupPlatform.Common.Models.AuthenticationModels
{
    public class RegisterData
    {
        public User User { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
