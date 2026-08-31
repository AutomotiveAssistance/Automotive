// Automotive.Assistance.API.Repositories.Interfaces/ICustomerRepository.cs
using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<List<Customer>> GetAllActiveAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetByUserIdAsync(int userId);
        Task<Customer?> GetByCustomerCodeAsync(string customerCode);
        Task<Customer> CreateAsync(Customer entity);
        Task<Customer> UpdateAsync(Customer entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByUserIdAsync(int userId);
        Task<bool> ExistsByCustomerCodeAsync(string customerCode);
        Task<int> CountAsync();
        Task<List<Customer>> GetPagedAsync(int pageNumber, int pageSize);
        Task<List<Customer>> GetCustomersByDateOfBirthRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<Customer>> SearchAsync(string searchTerm);
    }
}