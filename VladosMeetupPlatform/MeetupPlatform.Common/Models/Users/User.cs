using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetupPlatform.Common.Models.Users
{
    public class User
    {
        public User()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        public bool IsCompany { get; set; }

        [Required]
        public bool IsUserBlocked { get; set; } = false;

        [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
        public int? Age { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}
