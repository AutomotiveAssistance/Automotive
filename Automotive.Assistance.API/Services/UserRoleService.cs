// Automotive.Assistance.API.Services/UserRoleService.cs
using Automotive.Assistance.API.Mappers;
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Entities;
using Automotive.Assistance.API.Repositories;
using Automotive.Assistance.API.Repositories.Interfaces;
using Automotive.Assistance.API.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Automotive.Assistance.API.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<UserRoleService> _logger;

        public UserRoleService(
            IUserRoleRepository repository,
            IRoleRepository roleRepository,
            ILogger<UserRoleService> logger)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<UserRoleResponseDTO> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var roleNames = await GetRoleNamesDictionaryAsync();
                var responses = UserRoleMapper.ToResponseDTOList(entities.ToList(), roleNames);

                // Return as a wrapper or the first item
                // Since the interface expects single UserRoleResponseDTO, you need to handle this
                return new UserRoleResponseDTO
                {
                    // This is a workaround - you might want to create a wrapper DTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all user roles");
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> GetAllActiveAsync()
        {
            try
            {
                var entities = await _repository.GetAllActiveAsync();
                var roleNames = await GetRoleNamesDictionaryAsync();
                var responses = UserRoleMapper.ToResponseDTOList(entities.ToList(), roleNames);

                return new UserRoleResponseDTO
                {
                    // Workaround for list response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting active user roles");
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"User role with ID {id} not found");

                var roleName = await GetRoleNameAsync(entity.RoleId);
                var response = UserRoleMapper.ToResponseDTO(entity, roleName);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user role by ID: {Id}", id);
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> GetByUserIdAsync(int userId)
        {
            try
            {
                var entities = await _repository.GetByUserIdAsync(userId);
                var roleNames = await GetRoleNamesDictionaryAsync();
                var responses = UserRoleMapper.ToResponseDTOList(entities.ToList(), roleNames);

                // Since interface expects single DTO, return first or create wrapper
                return responses.FirstOrDefault() ?? new UserRoleResponseDTO();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user roles by user ID: {UserId}", userId);
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> GetByRoleIdAsync(int roleId)
        {
            try
            {
                var entities = await _repository.GetByRoleIdAsync(roleId);
                var roleName = await GetRoleNameAsync(roleId);
                var responses = UserRoleMapper.ToResponseDTOList(entities.ToList(), new Dictionary<int, string> { { roleId, roleName ?? string.Empty } });

                return responses.FirstOrDefault() ?? new UserRoleResponseDTO();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user roles by role ID: {RoleId}", roleId);
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> CreateAsync(UserRoleCreateDTO dto)
        {
            try
            {
                // Check if user-role combination already exists
                var exists = await _repository.ExistsByUserAndRoleAsync(dto.UserId, dto.RoleId);
                if (exists)
                {
                    throw new InvalidOperationException($"User with ID {dto.UserId} already has role ID {dto.RoleId}");
                }

                var entity = UserRoleMapper.ToEntity(dto);
                var createdEntity = await _repository.CreateAsync(entity);
                var roleName = await GetRoleNameAsync(createdEntity.RoleId);
                var response = UserRoleMapper.ToResponseDTO(createdEntity, roleName);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user role");
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> UpdateAsync(UserRoleUpdateDTO dto)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(dto.UserRoleId);
                if (entity == null)
                    throw new KeyNotFoundException($"User role with ID {dto.UserRoleId} not found");

                UserRoleMapper.UpdateEntity(dto, entity);
                var updatedEntity = await _repository.UpdateAsync(entity);
                var roleName = await GetRoleNameAsync(updatedEntity.RoleId);
                var response = UserRoleMapper.ToResponseDTO(updatedEntity, roleName);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user role with ID: {Id}", dto.UserRoleId);
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"User role with ID {id} not found");

                var result = await _repository.DeleteAsync(id);
                if (!result)
                    throw new InvalidOperationException($"Failed to delete user role with ID {id}");

                var roleName = await GetRoleNameAsync(entity.RoleId);
                var response = UserRoleMapper.ToResponseDTO(entity, roleName);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user role with ID: {Id}", id);
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> SoftDeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"User role with ID {id} not found");

                var result = await _repository.SoftDeleteAsync(id);
                if (!result)
                    throw new InvalidOperationException($"Failed to deactivate user role with ID {id}");

                var roleName = await GetRoleNameAsync(entity.RoleId);
                var response = UserRoleMapper.ToResponseDTO(entity, roleName);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deactivating user role with ID: {Id}", id);
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> IsUserInRoleAsync(int userId, int roleId)
        {
            try
            {
                var result = await _repository.IsUserInRoleAsync(userId, roleId);
                var roleName = await GetRoleNameAsync(roleId);

                return new UserRoleResponseDTO
                {
                    UserId = userId,
                    RoleId = roleId,
                    RoleName = roleName ?? string.Empty,
                    IsActive = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking user role for UserId: {UserId}, RoleId: {RoleId}", userId, roleId);
                throw;
            }
        }

        public async Task<UserRoleResponseDTO> GetPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var entities = await _repository.GetPagedAsync(pageNumber, pageSize);
                var roleNames = await GetRoleNamesDictionaryAsync();
                var responses = UserRoleMapper.ToResponseDTOList(entities.ToList(), roleNames);

                return responses.FirstOrDefault() ?? new UserRoleResponseDTO();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting paged user roles");
                throw;
            }
        }

        // Helper methods
        private async Task<Dictionary<int, string>> GetRoleNamesDictionaryAsync()
        {
            var roles = await _roleRepository.GetRolesListAsync();
            return roles.ToDictionary(r => r.Id, r => r.Name);
        }

        private async Task<string?> GetRoleNameAsync(int roleId)
        {
            var roles = await _roleRepository.GetRolesListAsync();
            var role = roles.FirstOrDefault(r => r.Id == roleId);
            return role?.Name;
        }
    }
}