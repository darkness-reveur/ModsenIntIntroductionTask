using MeetupPlatform.Common.Models.MeetUps;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetupPlatform.Common.Models.Users
{
    public class Organizer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int MeetupId { get; set; }

        [ForeignKey("MeetupId")]
        public virtual Meetup Meetup { get; set; }
    }
}
