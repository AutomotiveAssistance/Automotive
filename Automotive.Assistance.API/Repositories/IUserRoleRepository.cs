// Automotive.Assistance.API.Repositories/Interfaces/IUserRoleRepository.cs
using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<IEnumerable<UserRole>> GetAllActiveAsync();
        Task<UserRole?> GetByIdAsync(int id);
        Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserRole>> GetByRoleIdAsync(int roleId);
        Task<UserRole?> GetByUserAndRoleIdAsync(int userId, int roleId);
        Task<bool> IsUserInRoleAsync(int userId, int roleId);
        Task<UserRole> CreateAsync(UserRole entity);
        Task<UserRole> UpdateAsync(UserRole entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByUserAndRoleAsync(int userId, int roleId);
        Task<int> CountByRoleAsync(int roleId);
        Task<int> CountByUserAsync(int userId);
        Task<IEnumerable<UserRole>> GetPagedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<UserRole>> GetByUserIdsAsync(List<int> userIds);
        Task<IEnumerable<UserRole>> GetByRoleIdsAsync(List<int> roleIds);
    }
}