using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Automotive.Assistance.API.Models.Entities
{
    [Table("UserRole")]
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

    }
}
