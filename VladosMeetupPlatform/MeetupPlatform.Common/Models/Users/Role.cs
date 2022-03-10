using System.ComponentModel.DataAnnotations;

namespace MeetupPlatform.Common.Models.Users
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
