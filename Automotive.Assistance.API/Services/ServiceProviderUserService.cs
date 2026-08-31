using Automotive.Assistance.API.Mappers;
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Repositories;

namespace Automotive.Assistance.API.Services
{
    public class ServiceProviderUserService
        : IServiceProviderUserService
    {
        private readonly IServiceProviderUserRepository _serviceProviderUserRepository;

        private readonly IServiceProviderRepository _serviceProviderRepository;

        private readonly IUserRepository _userRepository;

        public ServiceProviderUserService(IServiceProviderUserRepository serviceProviderUserRepository, IServiceProviderRepository serviceProviderRepository, IUserRepository userRepository)
        {
            _serviceProviderUserRepository = serviceProviderUserRepository;

            _serviceProviderRepository = serviceProviderRepository;

            _userRepository = userRepository;
        }

        public async Task<List<ServiceProviderUserResponseDTO>> GetServiceProviderUsersListAsync()
        {
            var entities = await _serviceProviderUserRepository.GetServiceProviderUsersListAsync();

            return ServiceProviderUserMapper.ToResponseDTOList(entities);
        }

        public async Task<List<ServiceProviderUserResponseDTO>> GetUsersByServiceProviderIdAsync(int serviceProviderId)
        {
            var entities = await _serviceProviderUserRepository.GetUsersByServiceProviderIdAsync(serviceProviderId);

            return ServiceProviderUserMapper.ToResponseDTOList(entities);
        }

        public async Task<List<ServiceProviderUserResponseDTO>> GetServiceProvidersByUserIdAsync(int userId)
        {
            var entities = await _serviceProviderUserRepository.GetServiceProvidersByUserIdAsync(userId);

            return ServiceProviderUserMapper.ToResponseDTOList(entities);
        }



        public async Task<ServiceProviderUserResponseDTO?> GetServiceProviderUserByIdAsync(int serviceProviderUserId)
        {
            var entity = await _serviceProviderUserRepository.GetServiceProviderUserByIdAsync(serviceProviderUserId);

            if (entity == null)
                return null;

            return ServiceProviderUserMapper.ToResponseDTO(entity);
        }

        public async Task<ServiceProviderUserResponseDTO?> GetPrimaryContactAsync(int serviceProviderId)
        {
            var entity = await _serviceProviderUserRepository.GetPrimaryContactAsync(serviceProviderId);

            if (entity == null)
                return null;

            return ServiceProviderUserMapper.ToResponseDTO(entity);
        }

        public async Task<ServiceProviderUserResponseDTO> CreateServiceProviderUserAsync(ServiceProviderUserCreateDTO dto)
        {
            // Check Service Provider
            var serviceProvider = await _serviceProviderRepository.GetServiceProviderByIdAsync(dto.ServiceProviderId);

            if (serviceProvider == null)
            {
                throw new InvalidOperationException(
                    "Service provider not found.");
            }

            // Check User
            var user = await _userRepository.GetUserByIdAsync(dto.UserId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Check duplicate mapping
            var exists = await _serviceProviderUserRepository.ServiceProviderUserExistsAsync(dto.ServiceProviderId, dto.UserId);

            if (exists)
            {
                throw new InvalidOperationException(
                    "User is already assigned to this service provider.");
            }

            // If this user is primary contact,
            // remove existing primary contact.
            if (dto.IsPrimaryContact)
            {
                var existingPrimary = await _serviceProviderUserRepository.GetPrimaryContactAsync(dto.ServiceProviderId);

                if (existingPrimary != null)
                {
                    existingPrimary.IsPrimaryContact = false;
                    existingPrimary.UpdatedAt =
                        DateTime.UtcNow;

                    await _serviceProviderUserRepository
                        .UpdateServiceProviderUserAsync(
                            existingPrimary);
                }
            }

            var entity = ServiceProviderUserMapper.ToEntity(dto);

            var created = await _serviceProviderUserRepository.CreateServiceProviderUserAsync(entity);

            created.User = user;
            created.ServiceProvider = serviceProvider;

            return ServiceProviderUserMapper.ToResponseDTO(created);
        }

        public async Task<ServiceProviderUserResponseDTO?> UpdateServiceProviderUserAsync(int serviceProviderUserId, ServiceProviderUserUpdateDTO dto)
        {
            var entity = await _serviceProviderUserRepository.GetServiceProviderUserByIdAsync(serviceProviderUserId);

            if (entity == null)
                return null;

            if (dto.IsPrimaryContact)
            {
                var existingPrimary = await _serviceProviderUserRepository.GetPrimaryContactAsync(entity.ServiceProviderId);

                if (existingPrimary != null &&
                    existingPrimary.ServiceProviderUserId
                    != serviceProviderUserId)
                {
                    existingPrimary.IsPrimaryContact = false;
                    existingPrimary.UpdatedAt =
                        DateTime.UtcNow;

                    await _serviceProviderUserRepository.UpdateServiceProviderUserAsync(existingPrimary);
                }
            }

            ServiceProviderUserMapper.UpdateEntity(dto, entity);

            var updated = await _serviceProviderUserRepository.UpdateServiceProviderUserAsync(entity);

            return ServiceProviderUserMapper
                .ToResponseDTO(updated);
        }

        public async Task<bool>
            DeleteServiceProviderUserAsync(
                int serviceProviderUserId)
        {
            return await _serviceProviderUserRepository
                .DeleteServiceProviderUserAsync(
                    serviceProviderUserId);
        }

        public async Task<bool>
            UpdateStatusAsync(
                int serviceProviderUserId,
                bool isActive)
        {
            return await _serviceProviderUserRepository
                .UpdateStatusAsync(
                    serviceProviderUserId,
                    isActive);
        }

        public async Task<bool>
            UpdatePrimaryContactAsync(
                int serviceProviderUserId,
                bool isPrimaryContact)
        {
            var entity =
                await _serviceProviderUserRepository
                    .GetServiceProviderUserByIdAsync(
                        serviceProviderUserId);

            if (entity == null)
                return false;

            if (isPrimaryContact)
            {
                var existingPrimary =
                    await _serviceProviderUserRepository
                        .GetPrimaryContactAsync(
                            entity.ServiceProviderId);

                if (existingPrimary != null &&
                    existingPrimary.ServiceProviderUserId
                    != serviceProviderUserId)
                {
                    existingPrimary.IsPrimaryContact = false;
                    existingPrimary.UpdatedAt =
                        DateTime.UtcNow;

                    await _serviceProviderUserRepository
                        .UpdateServiceProviderUserAsync(
                            existingPrimary);
                }
            }

            return await _serviceProviderUserRepository
                .UpdatePrimaryContactAsync(
                    serviceProviderUserId,
                    isPrimaryContact);
        }
    }
}