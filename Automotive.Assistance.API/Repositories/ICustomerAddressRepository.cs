// Automotive.Assistance.API.Repositories.Interfaces/ICustomerAddressRepository.cs
using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Repositories.Interfaces
{
    public interface ICustomerAddressRepository
    {
        // Get All
        Task<List<CustomerAddress>> GetAllAsync();
        Task<List<CustomerAddress>> GetAllActiveAsync();

        // Get by ID
        Task<CustomerAddress?> GetByIdAsync(int id);

        // Get by Customer
        Task<List<CustomerAddress>> GetByCustomerIdAsync(int customerId);
        Task<List<CustomerAddress>> GetActiveAddressesByCustomerIdAsync(int customerId);

        // Get Default Address
        Task<CustomerAddress?> GetDefaultAddressByCustomerIdAsync(int customerId);

        // Check Existence
        Task<bool> ExistsAsync(int id);
        Task<bool> HasDefaultAddressAsync(int customerId);

        // CRUD Operations
        Task<CustomerAddress> CreateAsync(CustomerAddress entity);
        Task<CustomerAddress> UpdateAsync(CustomerAddress entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);

        // Bulk Operations
        Task<bool> RemoveDefaultFlagForCustomerAsync(int customerId, int excludeAddressId = 0);
        Task<bool> UpdateStatusAsync(int id, bool isActive);
        Task<int> GetAddressCountByCustomerIdAsync(int customerId);
    }
}