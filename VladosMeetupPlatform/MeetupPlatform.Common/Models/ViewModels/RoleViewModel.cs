using MeetupPlatform.Common.Models.Users;

namespace MeetupPlatform.Common.Models.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
