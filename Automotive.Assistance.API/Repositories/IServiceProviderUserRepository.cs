using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Repositories
{
    public interface IServiceProviderUserRepository
    {
        Task<List<ServiceProviderUser>> GetServiceProviderUsersListAsync();

        Task<List<ServiceProviderUser>> GetUsersByServiceProviderIdAsync(int serviceProviderId);

        Task<List<ServiceProviderUser>> GetServiceProvidersByUserIdAsync(int userId);

        Task<ServiceProviderUser?> GetServiceProviderUserByIdAsync(int serviceProviderUserId);

        Task<ServiceProviderUser?> GetServiceProviderUserAsync(int serviceProviderId, int userId);

        Task<ServiceProviderUser?> GetPrimaryContactAsync(int serviceProviderId);

        Task<ServiceProviderUser> CreateServiceProviderUserAsync(ServiceProviderUser serviceProviderUser);

        Task<ServiceProviderUser> UpdateServiceProviderUserAsync(ServiceProviderUser serviceProviderUser);

        Task<bool> DeleteServiceProviderUserAsync(int serviceProviderUserId);

        Task<bool> ServiceProviderUserExistsAsync(int serviceProviderId, int userId);

        Task<bool> UpdateStatusAsync(int serviceProviderUserId, bool isActive);

        Task<bool> UpdatePrimaryContactAsync(int serviceProviderUserId, bool isPrimaryContact);
    }
}