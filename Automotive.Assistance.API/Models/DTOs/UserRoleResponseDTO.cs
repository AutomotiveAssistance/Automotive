namespace Automotive.Assistance.API.Models.DTOs
{
    public class UserRoleResponseDTO
    {
        public int UserRoleId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
