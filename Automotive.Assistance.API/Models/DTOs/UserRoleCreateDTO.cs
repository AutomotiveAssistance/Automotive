using System.ComponentModel.DataAnnotations;

namespace Automotive.Assistance.API.Models.DTOs
{
    public class UserRoleCreateDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
