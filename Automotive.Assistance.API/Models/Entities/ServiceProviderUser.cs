using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Automotive.Assistance.API.Models.Entities
{
    [Table("ServiceProviderUser")]
    public class ServiceProviderUser
    {
        [Key]
        public int ServiceProviderUserId { get; set; }

        public int ServiceProviderId { get; set; }

        public int UserId { get; set; }

        public bool IsPrimaryContact { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
       
        public ServiceProvider? ServiceProvider { get; set; }

        public User? User { get; set; }
    }
}