using MeetupPlatform.Common.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetupPlatform.Common.Models.MeetUps
{
    public class Step
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int MeetupId { get; set; }

        [ForeignKey("MeetUpId")]
        public virtual Meetup Meetup { get; set; }

        public string? Description { get; set; }
              
        public int? UserSpeakerId { get; set; }

        [ForeignKey("SpeakerId")]
        public virtual User? UserSpeaker { get; set; }
    }
}
