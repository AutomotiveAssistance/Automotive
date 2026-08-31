// Automotive.Assistance.API.Services.Interfaces/ICustomerService.cs
using Automotive.Assistance.API.Models.DTOs;

namespace Automotive.Assistance.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerListResponseDTO> GetAllAsync();
        Task<CustomerListResponseDTO> GetAllActiveAsync();
        Task<CustomerResponseDTO> GetByIdAsync(int id);
        Task<CustomerResponseDTO> GetByUserIdAsync(int userId);
        Task<CustomerResponseDTO> GetByCustomerCodeAsync(string customerCode);
        Task<CustomerResponseDTO> CreateAsync(CustomerCreateDTO dto);
        Task<CustomerResponseDTO> UpdateAsync(CustomerUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByUserIdAsync(int userId);
        Task<int> GetTotalCountAsync();
        Task<CustomerListResponseDTO> GetPagedAsync(int pageNumber, int pageSize);
        Task<CustomerListResponseDTO> SearchAsync(string searchTerm);
        Task<CustomerListResponseDTO> GetCustomersByDateOfBirthRangeAsync(DateTime startDate, DateTime endDate);
        Task<bool> ActivateCustomerAsync(int id);
        Task<bool> DeactivateCustomerAsync(int id);
    }
}