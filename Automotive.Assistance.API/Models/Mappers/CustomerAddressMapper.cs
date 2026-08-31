// Automotive.Assistance.API.Mappers/CustomerAddressMapper.cs
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Mappers
{
    public static class CustomerAddressMapper
    {
        public static CustomerAddressResponseDTO ToResponseDTO(
            CustomerAddress entity,
            string? customerName = null)
        {
            return new CustomerAddressResponseDTO
            {
                CustomerAddressId = entity.CustomerAddressId,
                CustomerId = entity.CustomerId,
                CustomerName = customerName ?? entity.Customer?.CustomerCode,
                AddressType = entity.AddressType,
                AddressLine1 = entity.AddressLine1,
                AddressLine2 = entity.AddressLine2,
                City = entity.City,
                State = entity.State,
                Country = entity.Country,
                Pincode = entity.Pincode,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                IsDefault = entity.IsDefault,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };
        }

        public static List<CustomerAddressResponseDTO> ToResponseDTOList(
            List<CustomerAddress> entities,
            Dictionary<int, string>? customerNames = null)
        {
            return entities.Select(entity =>
            {
                var customerName = customerNames != null && customerNames.ContainsKey(entity.CustomerId)
                    ? customerNames[entity.CustomerId]
                    : entity.Customer?.CustomerCode;
                return ToResponseDTO(entity, customerName);
            }).ToList();
        }

        public static CustomerAddress ToEntity(CustomerAddressCreateDTO dto)
        {
            return new CustomerAddress
            {
                CustomerId = dto.CustomerId,
                AddressType = dto.AddressType,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                Pincode = dto.Pincode,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                IsDefault = dto.IsDefault,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM"
            };
        }

        public static void UpdateEntity(CustomerAddressUpdateDTO dto, CustomerAddress entity)
        {
            entity.AddressType = dto.AddressType;
            entity.AddressLine1 = dto.AddressLine1;
            entity.AddressLine2 = dto.AddressLine2;
            entity.City = dto.City;
            entity.State = dto.State;
            entity.Country = dto.Country;
            entity.Pincode = dto.Pincode;
            entity.Latitude = dto.Latitude;
            entity.Longitude = dto.Longitude;
            entity.IsDefault = dto.IsDefault;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "SYSTEM";
        }

        public static void UpdateEntityForSoftDelete(CustomerAddress entity)
        {
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "SYSTEM";
        }
    }
}