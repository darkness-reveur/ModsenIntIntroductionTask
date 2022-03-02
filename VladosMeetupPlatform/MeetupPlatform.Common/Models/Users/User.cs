using MeetupPlatform.Common.Models.HelperModels;
using MeetupPlatform.Common.Models.MeetUps;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetupPlatform.Common.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool IsCompany { get; set; }

        [Required]
        public bool IsUserBlocked { get; set; } = false;

        public int? Age { get; set; }
        
        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual List<MeetupVisitor> MeetupVisitors { get; set; }

        public virtual List<Step> Steps { get; set; }
    }
}
