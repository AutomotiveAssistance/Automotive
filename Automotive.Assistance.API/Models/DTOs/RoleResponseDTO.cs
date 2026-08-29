namespace Automotive.Assistance.API.Models.DTOs
{
    public class RoleResponseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
