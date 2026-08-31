using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Models.Mappers
{
    public static class UserMapper
    {
        public static UserResponseDTO ToResponseDTO(User user)
        {
            return new UserResponseDTO
            {
                UserId = user.UserId,

                FirstName = user.FirstName,
                LastName = user.LastName,

                MobileNumber = user.MobileNumber,
                Email = user.Email,

                IsActive = user.IsActive,
                IsVerified = user.IsVerified,

                LastLoginAt = user.LastLoginAt,
                CreatedAt = user.CreatedAt
            };
        }

        public static List<UserResponseDTO> ToResponseDTOList(
            List<User> users)
        {
            return users
                .Select(ToResponseDTO)
                .ToList();
        }

        public static void UpdateEntity(
            UserUpdateDTO dto,
            User entity)
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;

            entity.MobileNumber = dto.MobileNumber;
            entity.Email = dto.Email;

            entity.IsActive = dto.IsActive;

            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
