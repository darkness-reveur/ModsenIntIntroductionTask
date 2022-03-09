using MeetupPlatform.Common.Models.MeetUps;
using MeetupPlatform.Common.Models.Users;

namespace MeetupPlatform.Common.Models.ViewModels
{
    public class MeetupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int CountOfVisitors { get; set; }

        public virtual UserViewModel Organizer { get; set; }

        public string? Description { get; set; }

        public bool IsMeetForAuthorizedUsers { get; set; }

        public virtual Place? Place { get; set; }

        public virtual List<Step>? Steps { get; set; }

        public virtual List<Follower>? Followers { get; set; }

        public virtual List<UserViewModel>? UserVisitors { get; set; }
    }
}
