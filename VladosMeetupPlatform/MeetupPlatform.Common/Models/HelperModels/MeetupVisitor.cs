using MeetupPlatform.Common.Models.MeetUps;
using MeetupPlatform.Common.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetupPlatform.Common.Models.HelperModels
{
    public class MeetupVisitor
    {
        [Key]
        public int Id { get; set; }

        public int MeetupId { get; set; }

        [ForeignKey("MeetupId")]
        public virtual Meetup Meetup { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
