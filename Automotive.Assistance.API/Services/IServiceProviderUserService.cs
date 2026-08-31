using Automotive.Assistance.API.Models.DTOs;

namespace Automotive.Assistance.API.Services
{
    public interface IServiceProviderUserService
    {
        Task<List<ServiceProviderUserResponseDTO>> GetServiceProviderUsersListAsync();

        Task<List<ServiceProviderUserResponseDTO>> GetUsersByServiceProviderIdAsync(int serviceProviderId);

        Task<List<ServiceProviderUserResponseDTO>> GetServiceProvidersByUserIdAsync(int userId);

        Task<ServiceProviderUserResponseDTO?> GetServiceProviderUserByIdAsync(int serviceProviderUserId);

        Task<ServiceProviderUserResponseDTO?> GetPrimaryContactAsync(int serviceProviderId);

        Task<ServiceProviderUserResponseDTO> CreateServiceProviderUserAsync(ServiceProviderUserCreateDTO dto);

        Task<ServiceProviderUserResponseDTO?> UpdateServiceProviderUserAsync(int serviceProviderUserId, ServiceProviderUserUpdateDTO dto);

        Task<bool> DeleteServiceProviderUserAsync(int serviceProviderUserId);

        Task<bool> UpdateStatusAsync(int serviceProviderUserId, bool isActive);

        Task<bool> UpdatePrimaryContactAsync(int serviceProviderUserId, bool isPrimaryContact);
    }
}