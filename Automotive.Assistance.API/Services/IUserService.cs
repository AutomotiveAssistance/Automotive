using Automotive.Assistance.API.Models.DTOs;

namespace Automotive.Assistance.API.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetUsersListAsync();

        Task<UserResponseDTO?> GetUserByIdAsync(int userId);

        Task<UserResponseDTO> CreateUserAsync(UserCreateDTO userCreateDTO);

        Task<UserResponseDTO?> UpdateUserAsync(
            int userId,
            UserUpdateDTO userUpdateDTO);

        Task<bool> DeleteUserAsync(int userId);

        Task<bool> UpdateUserStatusAsync(
            int userId,
            bool isActive);

        Task<bool> VerifyUserAsync(
            int userId,
            bool isVerified);

        Task<bool> UpdateLastLoginAsync(int userId);
    }
}
