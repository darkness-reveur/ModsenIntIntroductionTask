using MeetupPlatform.Common.Models.MeetUps;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetupPlatform.Common.Models.Users
{
    public class Follower
    {
        [Key]
        public string Email { get; set; }

        [Required]
        public int MeetupId { get; set; }

        [ForeignKey("MeetupId")]
        public virtual Meetup Meetup { get; set; }
    }
}
