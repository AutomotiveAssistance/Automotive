using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Entities;
using Automotive.Assistance.API.Models.Mappers;
using Automotive.Assistance.API.Repositories;

namespace Automotive.Assistance.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDTO>> GetUsersListAsync()
        {
            var users = await _userRepository.GetUsersListAsync();

            return UserMapper.ToResponseDTOList(users);
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository
                .GetUserByIdAsync(userId);

            if (user == null)
                return null;

            return UserMapper.ToResponseDTO(user);
        }

        public async Task<UserResponseDTO> CreateUserAsync(
            UserCreateDTO userCreateDTO)
        {
            var mobileExists =
                await _userRepository
                    .MobileNumberExistsAsync(
                        userCreateDTO.MobileNumber);

            if (mobileExists)
            {
                throw new InvalidOperationException(
                    "Mobile number already registered.");
            }

            if (!string.IsNullOrWhiteSpace(userCreateDTO.Email))
            {
                var emailExists =
                    await _userRepository
                        .EmailExistsAsync(userCreateDTO.Email);

                if (emailExists)
                {
                    throw new InvalidOperationException(
                        "Email already registered.");
                }
            }

            var user = new User
            {
                FirstName = userCreateDTO.FirstName,
                LastName = userCreateDTO.LastName,

                MobileNumber = userCreateDTO.MobileNumber,
                Email = userCreateDTO.Email,

                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDTO.Password),

                IsActive = true,
                IsVerified = false,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdUser =
                await _userRepository.CreateUserAsync(user);

            return UserMapper.ToResponseDTO(createdUser);
        }

        public async Task<UserResponseDTO?> UpdateUserAsync(
            int userId,
            UserUpdateDTO userUpdateDTO)
        {
            var user =
                await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
                return null;

            // Check duplicate mobile number
            var existingMobile =
                await _userRepository
                    .GetUserByMobileNumberAsync(
                        userUpdateDTO.MobileNumber);

            if (existingMobile != null &&
                existingMobile.UserId != userId)
            {
                throw new InvalidOperationException(
                    "Mobile number already registered.");
            }

            // Check duplicate email
            if (!string.IsNullOrWhiteSpace(userUpdateDTO.Email))
            {
                var existingEmail =
                    await _userRepository
                        .GetUserByEmailAsync(userUpdateDTO.Email);

                if (existingEmail != null &&
                    existingEmail.UserId != userId)
                {
                    throw new InvalidOperationException(
                        "Email already registered.");
                }
            }

            UserMapper.UpdateEntity(userUpdateDTO, user);

            var updatedUser =
                await _userRepository.UpdateUserAsync(user);

            return UserMapper.ToResponseDTO(updatedUser);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository
                .DeleteUserAsync(userId);
        }

        public async Task<bool> UpdateUserStatusAsync(
            int userId,
            bool isActive)
        {
            return await _userRepository
                .UpdateUserStatusAsync(userId, isActive);
        }

        public async Task<bool> VerifyUserAsync(
            int userId,
            bool isVerified)
        {
            return await _userRepository
                .VerifyUserAsync(userId, isVerified);
        }

        public async Task<bool> UpdateLastLoginAsync(int userId)
        {
            return await _userRepository
                .UpdateLastLoginAsync(userId);
        }
    }
}