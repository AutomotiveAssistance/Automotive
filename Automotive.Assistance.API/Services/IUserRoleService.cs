
using Automotive.Assistance.API.Models.DTOs;

namespace Automotive.Assistance.API.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRoleResponseDTO> GetAllAsync();
        Task<UserRoleResponseDTO> GetAllActiveAsync();
        Task<UserRoleResponseDTO> GetByIdAsync(int id);
        Task<UserRoleResponseDTO> GetByUserIdAsync(int userId);
        Task<UserRoleResponseDTO> GetByRoleIdAsync(int roleId);
        Task<UserRoleResponseDTO> CreateAsync(UserRoleCreateDTO dto);
        Task<UserRoleResponseDTO> UpdateAsync(UserRoleUpdateDTO dto);
        Task<UserRoleResponseDTO> DeleteAsync(int id);
        Task<UserRoleResponseDTO> SoftDeleteAsync(int id);
        Task<UserRoleResponseDTO> IsUserInRoleAsync(int userId, int roleId);
        Task<UserRoleResponseDTO> GetPagedAsync(int pageNumber, int pageSize);
    }
}