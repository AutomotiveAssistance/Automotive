using System.ComponentModel.DataAnnotations;

namespace Automotive.Assistance.API.Models.DTOs
{
    public class UserUpdateDTO
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        public bool IsActive { get; set; }
    }
}
