using System.ComponentModel.DataAnnotations;

namespace Automotive.Assistance.API.Models.DTOs
{
    public class ServiceProviderUserCreateDTO
    {
        [Required]
        public int ServiceProviderId { get; set; }

        [Required]
        public int UserId { get; set; }

        public bool IsPrimaryContact { get; set; }
    }
}