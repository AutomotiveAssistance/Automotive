using Automotive.Assistance.API.Models.Entities;

namespace Automotive.Assistance.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersListAsync();

        Task<User?> GetUserByIdAsync(int userId);

        Task<User?> GetUserByMobileNumberAsync(string mobileNumber);

        Task<User?> GetUserByEmailAsync(string email);

        Task<User> CreateUserAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int userId);

        Task<bool> UserExistsAsync(int userId);

        Task<bool> MobileNumberExistsAsync(string mobileNumber);

        Task<bool> EmailExistsAsync(string email);

        Task<bool> UpdateUserStatusAsync(int userId, bool isActive);

        Task<bool> VerifyUserAsync(int userId, bool isVerified);

        Task<bool> UpdateLastLoginAsync(int userId);
    }
}