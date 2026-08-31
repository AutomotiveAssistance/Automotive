using Automotive.Assistance.API.Models.DTOs;

namespace Automotive.Assistance.API.Models.Mappers
{
    public static class ServiceProviderMapper
    {
        public static ServiceProviderResponseDTO ToResponseDTO(Automotive.Assistance.API.Models.Entities.ServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                return null!;

            return new ServiceProviderResponseDTO
            {
                ServiceProviderId = serviceProvider.ServiceProviderId,
                ServiceProviderName = serviceProvider.ServiceProviderName,
                ServiceProviderCode = serviceProvider.ServiceProviderCode,

                ContactPersonName = serviceProvider.ContactPersonName,
                ContactNumber = serviceProvider.ContactNumber,
                Email = serviceProvider.Email,

                GSTNumber = serviceProvider.GSTNumber,
                RegistrationNumber = serviceProvider.RegistrationNumber,

                AddressLine1 = serviceProvider.AddressLine1,
                AddressLine2 = serviceProvider.AddressLine2,
                City = serviceProvider.City,
                State = serviceProvider.State,
                Pincode = serviceProvider.Pincode,

                Latitude = serviceProvider.Latitude,
                Longitude = serviceProvider.Longitude,

                IsVerified = serviceProvider.IsVerified,
                IsActive = serviceProvider.IsActive,

                CreatedAt = serviceProvider.CreatedAt,
                UpdatedAt = serviceProvider.UpdatedAt
            };
        }

        public static Automotive.Assistance.API.Models.Entities.ServiceProvider ToEntity(ServiceProviderCreateDTO dto)
        {
            if (dto == null)
                return null!;

            return new Automotive.Assistance.API.Models.Entities.ServiceProvider
            {
                ServiceProviderName = dto.ServiceProviderName,
                ServiceProviderCode = dto.ServiceProviderCode,

                ContactPersonName = dto.ContactPersonName,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email,

                GSTNumber = dto.GSTNumber,
                RegistrationNumber = dto.RegistrationNumber,

                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                Pincode = dto.Pincode,

                Latitude = dto.Latitude,
                Longitude = dto.Longitude,

                IsActive = true,
                IsVerified = false,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static void UpdateEntity(
            ServiceProviderUpdateDTO dto,
            Automotive.Assistance.API.Models.Entities.ServiceProvider entity)
        {
            entity.ServiceProviderName = dto.ServiceProviderName;

            entity.ContactPersonName = dto.ContactPersonName;
            entity.ContactNumber = dto.ContactNumber;
            entity.Email = dto.Email;

            entity.GSTNumber = dto.GSTNumber;
            entity.RegistrationNumber = dto.RegistrationNumber;

            entity.AddressLine1 = dto.AddressLine1;
            entity.AddressLine2 = dto.AddressLine2;
            entity.City = dto.City;
            entity.State = dto.State;
            entity.Pincode = dto.Pincode;

            entity.Latitude = dto.Latitude;
            entity.Longitude = dto.Longitude;

            entity.IsActive = dto.IsActive;

            entity.UpdatedAt = DateTime.UtcNow;
        }

        public static List<ServiceProviderResponseDTO> ToResponseDTOList(List<Automotive.Assistance.API.Models.Entities.ServiceProvider> serviceProviders)
        {
            return serviceProviders.Select(ToResponseDTO).ToList();
        }
    }
}
