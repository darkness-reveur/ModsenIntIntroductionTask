using MeetupPlatform.Common.Models.HelperModels;
using MeetupPlatform.Common.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetupPlatform.Common.Models.MeetUps
{
    public class Meetup
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
        public int CountOfVisitors { get; set; }

        [Required]
        public int OrganizerId { get; set; }

        [ForeignKey("OrganizerId")]
        public virtual Organizer Organizer { get; set; }

        public string? Description { get; set; }

        public bool IsMeetForAuthorizedUsers { get; set; }

        public int? PlaceId { get; set; }

        [ForeignKey("PlaceId")]
        public virtual Place? Place { get; set; }

        public virtual List<Step> Steps { get; set; }

        public virtual List<Follower> Followers { get; set; }

        public virtual List<MeetupVisitor> UserVisitors { get; set; }
    }
}
