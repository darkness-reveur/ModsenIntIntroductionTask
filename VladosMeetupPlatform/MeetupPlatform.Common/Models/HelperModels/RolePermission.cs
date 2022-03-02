using MeetupPlatform.Common.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetupPlatform.Common.Models.HelperModels
{
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        
        public int PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
    }
}
