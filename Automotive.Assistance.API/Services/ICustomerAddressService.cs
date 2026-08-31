// Automotive.Assistance.API.Services.Interfaces/ICustomerAddressService.cs
using Automotive.Assistance.API.Models.DTOs;

namespace Automotive.Assistance.API.Services.Interfaces
{
    public interface ICustomerAddressService
    {
        // Get All
        Task<CustomerAddressListResponseDTO> GetAllAsync();
        Task<CustomerAddressListResponseDTO> GetAllActiveAsync();

        // Get by ID
        Task<CustomerAddressResponseDTO> GetByIdAsync(int id);

        // Get by Customer
        Task<CustomerAddressListResponseDTO> GetByCustomerIdAsync(int customerId);
        Task<CustomerAddressListResponseDTO> GetActiveAddressesByCustomerIdAsync(int customerId);

        // Get Default Address
        Task<CustomerAddressResponseDTO> GetDefaultAddressAsync(int customerId);

        // Create
        Task<CustomerAddressResponseDTO> CreateAsync(CustomerAddressCreateDTO dto);

        // Update
        Task<CustomerAddressResponseDTO> UpdateAsync(CustomerAddressUpdateDTO dto);

        // Delete
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);

        // Status
        Task<bool> ActivateAddressAsync(int id);
        Task<bool> DeactivateAddressAsync(int id);
        Task<bool> SetDefaultAddressAsync(int customerId, int addressId);

        // Check Existence
        Task<bool> ExistsAsync(int id);
        Task<bool> HasDefaultAddressAsync(int customerId);

        // Statistics
        Task<int> GetAddressCountByCustomerIdAsync(int customerId);
    }
}