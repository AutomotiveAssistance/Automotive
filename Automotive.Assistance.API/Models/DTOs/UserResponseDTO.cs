namespace Automotive.Assistance.API.Models.DTOs
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        public string MobileNumber { get; set; } = string.Empty;

        public string? Email { get; set; }

        public bool IsActive { get; set; }

        public bool IsVerified { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
