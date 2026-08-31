using System.ComponentModel.DataAnnotations;

namespace Automotive.Assistance.API.Models.DTOs
{
    public class CustomerUpdateDTO
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        public string? CustomerCode { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactNumber { get; set; }

        public string? ProfileImageUrl { get; set; }

        public bool IsActive { get; set; }
    }
}