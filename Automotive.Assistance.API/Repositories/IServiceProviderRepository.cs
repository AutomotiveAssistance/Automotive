namespace Automotive.Assistance.API.Repositories
{
    public interface IServiceProviderRepository
    {
        // Get All
        Task<List<Automotive.Assistance.API.Models.Entities.ServiceProvider>> GetServiceProvidersListAsync();

        // Get By Id
        Task<Automotive.Assistance.API.Models.Entities.ServiceProvider?> GetServiceProviderByIdAsync(int serviceProviderId);

        // Get By Code
        Task<Automotive.Assistance.API.Models.Entities.ServiceProvider?> GetServiceProviderByCodeAsync(string serviceProviderCode);

        // Get By Name
        Task<Automotive.Assistance.API.Models.Entities.ServiceProvider?> GetServiceProviderByNameAsync(string serviceProviderName);

        // Create
        Task<Automotive.Assistance.API.Models.Entities.ServiceProvider> CreateServiceProviderAsync(Automotive.Assistance.API.Models.Entities.ServiceProvider serviceProvider);

        // Update
        Task<Automotive.Assistance.API.Models.Entities.ServiceProvider> UpdateServiceProviderAsync(Automotive.Assistance.API.Models.Entities.ServiceProvider serviceProvider);

        // Delete
        Task<bool> DeleteServiceProviderAsync(int serviceProviderId);

        // Exists
        Task<bool> ServiceProviderExistsAsync(int serviceProviderId);

        // Check Code Exists
        Task<bool> ServiceProviderCodeExistsAsync(string serviceProviderCode);

        // Check Name Exists
        Task<bool> ServiceProviderNameExistsAsync(string serviceProviderName);

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
