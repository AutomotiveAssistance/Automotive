// Automotive.Assistance.API.Mappers/UserRoleMapper.cs
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Mappers
{
    public static class UserRoleMapper
    {
        public static UserRoleResponseDTO ToResponseDTO(UserRole entity, string? roleName = null)
        {
            return new UserRoleResponseDTO
            {
                UserRoleId = entity.UserRoleId,
                UserId = entity.UserId,
                RoleId = entity.RoleId,
                RoleName = roleName ?? string.Empty,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt
            };
        }

        public static List<UserRoleResponseDTO> ToResponseDTOList(List<UserRole> entities, Dictionary<int, string>? roleNames = null)
        {
            return entities.Select(entity =>
            {
                var roleName = roleNames != null && roleNames.ContainsKey(entity.RoleId)
                    ? roleNames[entity.RoleId]
                    : null;
                return ToResponseDTO(entity, roleName);
            }).ToList();
        }

        public static UserRole ToEntity(UserRoleCreateDTO dto)
        {
            return new UserRole
            {
                UserId = dto.UserId,
                RoleId = dto.RoleId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM"
            };
        }

        public static void UpdateEntity(UserRoleUpdateDTO dto, UserRole entity)
        {
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "SYSTEM";
        }

        public static void UpdateEntityForSoftDelete(UserRole entity)
        {
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = "SYSTEM";
        }
    }
}