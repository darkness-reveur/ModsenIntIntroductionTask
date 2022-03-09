using MeetupPlatform.Common.Models.MeetUps;
using MeetupPlatform.Common.Models.Users;

namespace MeetupPlatform.Common.Models.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            Id = user.Id;

            Name = user.Name;

            Email = user.Email;

            IsCompany = user.IsCompany;

            IsUserBlocked = user.IsUserBlocked;

            Age = user.Age;

            RoleId = user.RoleId;
        }

        public UserViewModel()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsCompany { get; set; }

        public bool IsUserBlocked { get; set; } = false;

        public int? Age { get; set; }

        public int RoleId { get; set; }

        public virtual RoleViewModel Role { get; set; }

        public virtual List<Meetup>? Meetups { get; set; }
    }
}
