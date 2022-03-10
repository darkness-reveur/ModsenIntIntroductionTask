using System.ComponentModel.DataAnnotations;

namespace MeetupPlatform.Common.Models.Users
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
