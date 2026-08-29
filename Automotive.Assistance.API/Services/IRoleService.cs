using Automotive.Assistance.API.Models.DTOs;

namespace Automotive.Assistance.API.Services
{
    public interface IRoleService
    {
        Task<List<RoleResponseDTO>> GetRolesListAsync();
    }
}
