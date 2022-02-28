using MeetupPlatform.Common.Models.MeetUps;
using System.ComponentModel.DataAnnotations;

namespace MeetupPlatform.Common.Models.Users
{
    public class Follower
    {
        [Key]
        public string Email { get; set; }
    }
}
