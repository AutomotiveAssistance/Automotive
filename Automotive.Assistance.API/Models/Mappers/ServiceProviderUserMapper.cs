using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Mappers
{
    public static class ServiceProviderUserMapper
    {
        public static ServiceProviderUserResponseDTO
            ToResponseDTO(ServiceProviderUser entity)
        {
            return new ServiceProviderUserResponseDTO
            {
                ServiceProviderUserId = entity.ServiceProviderUserId,

                ServiceProviderId = entity.ServiceProviderId,

                ServiceProviderName = entity.ServiceProvider?.ServiceProviderName,

                UserId = entity.UserId,

                UserName = entity.User != null ? $"{entity.User.FirstName} {entity.User.LastName}" : null,

                MobileNumber = entity.User?.MobileNumber,

                Email = entity.User?.Email,

                IsPrimaryContact = entity.IsPrimaryContact,

                IsActive = entity.IsActive,

                CreatedAt = entity.CreatedAt
            };
        }

        public static List<ServiceProviderUserResponseDTO> ToResponseDTOList(List<ServiceProviderUser> entities)
        {
            return entities.Select(ToResponseDTO).ToList();
        }

        public static ServiceProviderUser ToEntity(ServiceProviderUserCreateDTO dto)
        {
            return new ServiceProviderUser
            {
                ServiceProviderId = dto.ServiceProviderId,

                UserId = dto.UserId,

                IsPrimaryContact = dto.IsPrimaryContact,

                IsActive = true,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static void UpdateEntity(ServiceProviderUserUpdateDTO dto, ServiceProviderUser entity)
        {
            entity.IsPrimaryContact = dto.IsPrimaryContact;

            entity.IsActive = dto.IsActive;

            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}