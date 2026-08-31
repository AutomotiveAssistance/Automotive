namespace Automotive.Assistance.API.Models.DTOs
{
    public class ServiceProviderResponseDTO
    {
        public int ServiceProviderId { get; set; }

        public string ServiceProviderName { get; set; } = string.Empty;

        public string? ServiceProviderCode { get; set; }

        public string? ContactPersonName { get; set; }

        public string? ContactNumber { get; set; }

        public string? Email { get; set; }

        public string? GSTNumber { get; set; }

        public string? RegistrationNumber { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Pincode { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public bool IsVerified { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}