// Automotive.Assistance.API.Models.DTOs/CustomerCreateDTO.cs
using System.ComponentModel.DataAnnotations;

namespace Automotive.Assistance.API.Models.DTOs
{
    public class CustomerCreateDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        public string? CustomerCode { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactNumber { get; set; }

        public string? ProfileImageUrl { get; set; }
    }
}

// Automotive.Assistance.API.Models.DTOs/CustomerUpdateDTO.cs




namespace Automotive.Assistance.API.Models.DTOs
{
    public class CustomerListResponseDTO
    {
        public List<CustomerResponseDTO> Customers { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}