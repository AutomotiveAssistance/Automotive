namespace Automotive.Assistance.API.Models.DTOs
{
    public class ServiceProviderUpdateDTO
    {
        public string ServiceProviderName { get; set; } = string.Empty;

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

        public bool IsActive { get; set; }
    }
}
