// Automotive.Assistance.API.Mappers/CustomerMapper.cs
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerResponseDTO ToResponseDTO(Customer entity, User? user = null)
        {
            return new CustomerResponseDTO
            {
                CustomerId = entity.CustomerId,
                UserId = entity.UserId,
                UserName = user != null ? $"{user.FirstName} {user.LastName}" : null,
                UserEmail = user?.Email,
                UserMobileNumber = user?.MobileNumber,
                CustomerCode = entity.CustomerCode,
                DateOfBirth = entity.DateOfBirth,
                Gender = entity.Gender,
                EmergencyContactName = entity.EmergencyContactName,
                EmergencyContactNumber = entity.EmergencyContactNumber,
                ProfileImageUrl = entity.ProfileImageUrl,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };
        }

        public static List<CustomerResponseDTO> ToResponseDTOList(
            List<Customer> entities,
            Dictionary<int, User>? users = null)
        {
            return entities.Select(entity =>
            {
                var user = users != null && users.ContainsKey(entity.UserId)
                    ? users[entity.UserId]
                    : null;
                return ToResponseDTO(entity, user);
            }).ToList();
        }

        public static Customer ToEntity(CustomerCreateDTO dto)
        {
            return new Customer
            {
                UserId = dto.UserId,
                CustomerCode = dto.CustomerCode,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                EmergencyContactName = dto.EmergencyContactName,
                EmergencyContactNumber = dto.EmergencyContactNumber,
                ProfileImageUrl = dto.ProfileImageUrl,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM"
            };
        }

        public static void UpdateEntity(CustomerUpdateDTO dto, Customer entity)
        {
            entity.CustomerCode = dto.CustomerCode;
            entity.DateOfBirth = dto.DateOfBirth;
            entity.Gender = dto.Gender;
            entity.EmergencyContactName = dto.EmergencyContactName;
            entity.EmergencyContactNumber = dto.EmergencyContactNumber;
            entity.ProfileImageUrl = dto.ProfileImageUrl;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "SYSTEM";
        }

        public static void UpdateEntityForSoftDelete(Customer entity)
        {
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "SYSTEM";
        }
    }
}