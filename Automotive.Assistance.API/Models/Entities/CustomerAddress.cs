// Automotive.Assistance.API.Models.Entities/CustomerAddress.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Automotive.Assistance.API.Models.Entities
{
    [Table("CustomerAddress")]
    public class CustomerAddress
    {
        [Key]
        public int CustomerAddressId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [MaxLength(50)]
        public string? AddressType { get; set; }

        [Required]
        [MaxLength(250)]
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

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        // Navigation Property
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}