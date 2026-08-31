using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Automotive.Assistance.API.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        public string MobileNumber { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? PasswordSalt { get; set; }

        public bool IsActive { get; set; }

        public bool IsVerified { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
