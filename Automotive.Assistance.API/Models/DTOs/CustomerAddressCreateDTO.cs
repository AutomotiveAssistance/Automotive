// Automotive.Assistance.API.Models.DTOs/CustomerAddressCreateDTO.cs
using System.ComponentModel.DataAnnotations;

namespace Automotive.Assistance.API.Models.DTOs
{
    public class CustomerAddressCreateDTO
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        [MaxLength(50)]
        public string? AddressType { get; set; }

        [Required(ErrorMessage = "AddressLine1 is required")]
        [MaxLength(250, ErrorMessage = "AddressLine1 cannot exceed 250 characters")]
        public string AddressLine1 { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? AddressLine2 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(20)]
        public string? Pincode { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public bool IsDefault { get; set; } = false;
    }
}

// Automotive.Assistance.API.Models.DTOs/CustomerAddressUpdateDTO.cs

namespace Automotive.Assistance.API.Models.DTOs
{
    public class CustomerAddressUpdateDTO
    {
        [Required(ErrorMessage = "CustomerAddressId is required")]
        public int CustomerAddressId { get; set; }

        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        [MaxLength(50)]
        public string? AddressType { get; set; }

        [Required(ErrorMessage = "AddressLine1 is required")]
        [MaxLength(250, ErrorMessage = "AddressLine1 cannot exceed 250 characters")]
        public string AddressLine1 { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? AddressLine2 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(20)]
        public string? Pincode { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }
    }
}

// Automotive.Assistance.API.Models.DTOs/CustomerAddressResponseDTO.cs
namespace Automotive.Assistance.API.Models.DTOs
{
    public class CustomerAddressResponseDTO
    {
        public int CustomerAddressId { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? AddressType { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Pincode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

// Automotive.Assistance.API.Models.DTOs/CustomerAddressListResponseDTO.cs
namespace Automotive.Assistance.API.Models.DTOs
{
    public class CustomerAddressListResponseDTO
    {
        public List<CustomerAddressResponseDTO> Addresses { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}