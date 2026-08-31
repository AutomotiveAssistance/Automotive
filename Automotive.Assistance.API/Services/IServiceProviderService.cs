using Automotive.Assistance.API.Models.DTOs;

namespace Automotive.Assistance.API.Services
{
    public interface IServiceProviderService
    {
        // Get All
        Task<List<ServiceProviderResponseDTO>> GetServiceProvidersListAsync();

        // Get By Id
        Task<ServiceProviderResponseDTO?> GetServiceProviderByIdAsync(
            int serviceProviderId);

        // Create
        Task<ServiceProviderResponseDTO> CreateServiceProviderAsync(
            ServiceProviderCreateDTO serviceProviderCreateDTO);

        // Update
        Task<ServiceProviderResponseDTO?> UpdateServiceProviderAsync(
            int serviceProviderId,
            ServiceProviderUpdateDTO serviceProviderUpdateDTO);

        // Delete
        Task<bool> DeleteServiceProviderAsync(int serviceProviderId);

        // Activate / Deactivate
        Task<bool> UpdateServiceProviderStatusAsync(
            int serviceProviderId,
            bool isActive);

        // Verify
        Task<bool> VerifyServiceProviderAsync(
            int serviceProviderId,
            bool isVerified);
    }
}