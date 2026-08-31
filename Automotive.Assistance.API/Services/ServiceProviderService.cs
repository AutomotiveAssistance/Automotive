using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Mappers;
using Automotive.Assistance.API.Repositories;

namespace Automotive.Assistance.API.Services
{
    public class ServiceProviderService : IServiceProviderService
    {
        private readonly IServiceProviderRepository _serviceProviderRepository;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IServiceProviderUserService _serviceProviderUserService;

        public ServiceProviderService(IServiceProviderRepository serviceProviderRepository,
            IUserService userService,
            IRoleService roleService,IServiceProviderUserService serviceProviderUserService)
        {
            _serviceProviderRepository = serviceProviderRepository;
            _userService = userService;
            _roleService = roleService;
            _serviceProviderUserService = serviceProviderUserService;
        }

        // =====================================================
        // GET ALL SERVICE PROVIDERS
        // =====================================================

        public async Task<List<ServiceProviderResponseDTO>> GetServiceProvidersListAsync()
        {
            var serviceProviders = await _serviceProviderRepository.GetServiceProvidersListAsync();

            return ServiceProviderMapper.ToResponseDTOList(serviceProviders);
        }


        // =====================================================
        // GET SERVICE PROVIDER BY ID
        // =====================================================

        public async Task<ServiceProviderResponseDTO?>
            GetServiceProviderByIdAsync(int serviceProviderId)
        {
            var serviceProvider =
                await _serviceProviderRepository
                    .GetServiceProviderByIdAsync(serviceProviderId);

            if (serviceProvider == null)
                return null;

            return ServiceProviderMapper
                .ToResponseDTO(serviceProvider);
        }


        // =====================================================
        // CREATE SERVICE PROVIDER
        // =====================================================

        public async Task<ServiceProviderResponseDTO> CreateServiceProviderAsync(ServiceProviderCreateDTO serviceProviderCreateDTO)
        {
            // Check duplicate name
            var existingServiceProvider = await _serviceProviderRepository.GetServiceProviderByNameAsync(serviceProviderCreateDTO.ServiceProviderName);

            if (existingServiceProvider != null)
            {
                throw new InvalidOperationException("Service provider with this name already exists.");
            }

            // Check duplicate code
            if (!string.IsNullOrWhiteSpace(
                serviceProviderCreateDTO.ServiceProviderCode))
            {
                var existingCode = await _serviceProviderRepository.ServiceProviderCodeExistsAsync(serviceProviderCreateDTO.ServiceProviderCode);

                if (existingCode)
                {
                    throw new InvalidOperationException(
                        "Service provider code already exists.");
                }
            }

            // Map DTO to Entity
            var serviceProvider = ServiceProviderMapper.ToEntity(serviceProviderCreateDTO);

            // Save
            var createdServiceProvider = await _serviceProviderRepository.CreateServiceProviderAsync(serviceProvider);


            //create admin user after creating service provider

            UserCreateDTO userCreateDTO = new UserCreateDTO()
            {
                Email = serviceProviderCreateDTO.Email,
                FirstName = serviceProviderCreateDTO.ContactPersonName,
                LastName = serviceProviderCreateDTO.ContactPersonName,
                MobileNumber = serviceProviderCreateDTO.ContactNumber,
                Password = "Admin@2026"
            };

            var user = await _userService.CreateUserAsync(userCreateDTO);


            //Create user rOLE 

            var roles = await _roleService.GetRolesListAsync();

            if (roles.Any())
            {
                var adminRole = roles.FirstOrDefault(x => x.Code == "ADMIN");

                UserRoleCreateDTO roleDTO = new UserRoleCreateDTO()
                {
                    RoleId = adminRole.Id,
                    UserId = user.UserId
                };
               

            }
            // Return Response
            return ServiceProviderMapper.ToResponseDTO(createdServiceProvider);
        }


        // =====================================================
        // UPDATE SERVICE PROVIDER
        // =====================================================

        public async Task<ServiceProviderResponseDTO?>
            UpdateServiceProviderAsync(
                int serviceProviderId,
                ServiceProviderUpdateDTO serviceProviderUpdateDTO)
        {
            var existingServiceProvider =
                await _serviceProviderRepository
                    .GetServiceProviderByIdAsync(serviceProviderId);

            if (existingServiceProvider == null)
                return null;

            // Update entity using mapper
            ServiceProviderMapper.UpdateEntity(
                serviceProviderUpdateDTO,
                existingServiceProvider);

            var updatedServiceProvider =
                await _serviceProviderRepository
                    .UpdateServiceProviderAsync(existingServiceProvider);

            return ServiceProviderMapper
                .ToResponseDTO(updatedServiceProvider);
        }


        // =====================================================
        // DELETE SERVICE PROVIDER
        // =====================================================

        public async Task<bool> DeleteServiceProviderAsync(
            int serviceProviderId)
        {
            return await _serviceProviderRepository
                .DeleteServiceProviderAsync(serviceProviderId);
        }


        // =====================================================
        // ACTIVATE / DEACTIVATE
        // =====================================================

        public async Task<bool> UpdateServiceProviderStatusAsync(
            int serviceProviderId,
            bool isActive)
        {
            return await _serviceProviderRepository
                .UpdateServiceProviderStatusAsync(
                    serviceProviderId,
                    isActive);
        }


        // =====================================================
        // VERIFY SERVICE PROVIDER
        // =====================================================

        public async Task<bool> VerifyServiceProviderAsync(
            int serviceProviderId,
            bool isVerified)
        {
            return await _serviceProviderRepository
                .VerifyServiceProviderAsync(
                    serviceProviderId,
                    isVerified);
        }
    }
}
