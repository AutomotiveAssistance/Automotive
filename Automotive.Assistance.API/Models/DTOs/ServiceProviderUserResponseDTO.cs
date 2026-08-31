namespace Automotive.Assistance.API.Models.DTOs
{
    public class ServiceProviderUserResponseDTO
    {
        public int ServiceProviderUserId { get; set; }

        public int ServiceProviderId { get; set; }

        public string? ServiceProviderName { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string? MobileNumber { get; set; }

        public string? Email { get; set; }

        public bool IsPrimaryContact { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}