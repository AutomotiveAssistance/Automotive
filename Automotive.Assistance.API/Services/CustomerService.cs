// Automotive.Assistance.API.Services/CustomerService.cs
using Automotive.Assistance.API.Mappers;
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Models.Entities;
using Automotive.Assistance.API.Repositories;
using Automotive.Assistance.API.Repositories.Interfaces;
using Automotive.Assistance.API.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Automotive.Assistance.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(
            ICustomerRepository repository,
            IUserRepository userRepository,
            ILogger<CustomerService> logger)
        {
            _repository = repository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<CustomerListResponseDTO> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var users = await GetUsersDictionaryAsync(entities);
                var responses = CustomerMapper.ToResponseDTOList(entities, users);

                return new CustomerListResponseDTO
                {
                    Customers = responses,
                    TotalCount = responses.Count,
                    PageNumber = 1,
                    PageSize = responses.Count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all customers");
                throw;
            }
        }

        public async Task<CustomerListResponseDTO> GetAllActiveAsync()
        {
            try
            {
                var entities = await _repository.GetAllActiveAsync();
                var users = await GetUsersDictionaryAsync(entities);
                var responses = CustomerMapper.ToResponseDTOList(entities, users);

                return new CustomerListResponseDTO
                {
                    Customers = responses,
                    TotalCount = responses.Count,
                    PageNumber = 1,
                    PageSize = responses.Count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting active customers");
                throw;
            }
        }

        public async Task<CustomerResponseDTO> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer with ID {id} not found");

                var user = await _userRepository.GetUserByIdAsync(entity.UserId);

                return CustomerMapper.ToResponseDTO(entity, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer by ID: {Id}", id);
                throw;
            }
        }

        public async Task<CustomerResponseDTO> GetByUserIdAsync(int userId)
        {
            try
            {
                var entity = await _repository.GetByUserIdAsync(userId);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer with UserId {userId} not found");

                var user = await _userRepository.GetUserByIdAsync(entity.UserId);
                return CustomerMapper.ToResponseDTO(entity, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer by UserId: {UserId}", userId);
                throw;
            }
        }

        public async Task<CustomerResponseDTO> GetByCustomerCodeAsync(string customerCode)
        {
            try
            {
                var entity = await _repository.GetByCustomerCodeAsync(customerCode);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer with CustomerCode {customerCode} not found");

                var user = await _userRepository.GetUserByIdAsync(entity.UserId);
                return CustomerMapper.ToResponseDTO(entity, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer by CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }

        public async Task<CustomerResponseDTO> CreateAsync(CustomerCreateDTO dto)
        {
            try
            {
                // Check if user exists
                var user = await _userRepository.GetUserByIdAsync(dto.UserId);
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {dto.UserId} not found");

                // Check if user already has a customer profile
                var exists = await _repository.ExistsByUserIdAsync(dto.UserId);
                if (exists)
                    throw new InvalidOperationException($"User with ID {dto.UserId} already has a customer profile");

                // Check if customer code is unique (if provided)
                if (!string.IsNullOrEmpty(dto.CustomerCode))
                {
                    var codeExists = await _repository.ExistsByCustomerCodeAsync(dto.CustomerCode);
                    if (codeExists)
                        throw new InvalidOperationException($"Customer code {dto.CustomerCode} already exists");
                }

                // Generate customer code if not provided
                if (string.IsNullOrEmpty(dto.CustomerCode))
                {
                    dto.CustomerCode = await GenerateCustomerCodeAsync();
                }

                var entity = CustomerMapper.ToEntity(dto);
                var createdEntity = await _repository.CreateAsync(entity);

                return CustomerMapper.ToResponseDTO(createdEntity, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer");
                throw;
            }
        }

        public async Task<CustomerResponseDTO> UpdateAsync(CustomerUpdateDTO dto)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(dto.CustomerId);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer with ID {dto.CustomerId} not found");

                // Check if customer code is unique (if changed)
                if (!string.IsNullOrEmpty(dto.CustomerCode) &&
                    dto.CustomerCode != entity.CustomerCode)
                {
                    var codeExists = await _repository.ExistsByCustomerCodeAsync(dto.CustomerCode);
                    if (codeExists)
                        throw new InvalidOperationException($"Customer code {dto.CustomerCode} already exists");
                }

                CustomerMapper.UpdateEntity(dto, entity);
                var updatedEntity = await _repository.UpdateAsync(entity);

                var user = await _userRepository.GetUserByIdAsync(updatedEntity.UserId);
                return CustomerMapper.ToResponseDTO(updatedEntity, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating customer with ID: {Id}", dto.CustomerId);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                if (!result)
                    throw new KeyNotFoundException($"Customer with ID {id} not found");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting customer with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            try
            {
                var result = await _repository.SoftDeleteAsync(id);
                if (!result)
                    throw new KeyNotFoundException($"Customer with ID {id} not found");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft deleting customer with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                return await _repository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence for customer ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsByUserIdAsync(int userId)
        {
            try
            {
                return await _repository.ExistsByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence for UserId: {UserId}", userId);
                throw;
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            try
            {
                return await _repository.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting total customer count");
                throw;
            }
        }

        public async Task<CustomerListResponseDTO> GetPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var entities = await _repository.GetPagedAsync(pageNumber, pageSize);
                var totalCount = await _repository.CountAsync();
                var users = await GetUsersDictionaryAsync(entities);
                var responses = CustomerMapper.ToResponseDTOList(entities, users);

                return new CustomerListResponseDTO
                {
                    Customers = responses,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting paged customers");
                throw;
            }
        }

        public async Task<CustomerListResponseDTO> SearchAsync(string searchTerm)
        {
            try
            {
                var entities = await _repository.SearchAsync(searchTerm);
                var users = await GetUsersDictionaryAsync(entities);
                var responses = CustomerMapper.ToResponseDTOList(entities, users);

                return new CustomerListResponseDTO
                {
                    Customers = responses,
                    TotalCount = responses.Count,
                    PageNumber = 1,
                    PageSize = responses.Count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching customers");
                throw;
            }
        }

        public async Task<CustomerListResponseDTO> GetCustomersByDateOfBirthRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var entities = await _repository.GetCustomersByDateOfBirthRangeAsync(startDate, endDate);
                var users = await GetUsersDictionaryAsync(entities);
                var responses = CustomerMapper.ToResponseDTOList(entities, users);

                return new CustomerListResponseDTO
                {
                    Customers = responses,
                    TotalCount = responses.Count,
                    PageNumber = 1,
                    PageSize = responses.Count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customers by date of birth range");
                throw;
            }
        }

        public async Task<bool> ActivateCustomerAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer with ID {id} not found");

                entity.IsActive = true;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = "SYSTEM";

                await _repository.UpdateAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while activating customer with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeactivateCustomerAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Customer with ID {id} not found");

                entity.IsActive = false;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = "SYSTEM";

                await _repository.UpdateAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deactivating customer with ID: {Id}", id);
                throw;
            }
        }

        // Helper methods
        private async Task<Dictionary<int, User>> GetUsersDictionaryAsync(List<Customer> customers)
        {
            if (!customers.Any())
                return new Dictionary<int, User>();

            var userIds = customers.Select(c => c.UserId).Distinct().ToList();
            
            var users = await _userRepository.GetUsersListAsync();

            var filteredusers = users.Where(p => userIds.Contains(p.UserId)).ToList();

            return filteredusers.ToDictionary(u => u.UserId, u => u);
        }

        private async Task<string> GenerateCustomerCodeAsync()
        {
            var count = await _repository.CountAsync();
            var code = $"CUS-{DateTime.Now:yyyyMMdd}-{(count + 1).ToString("D4")}";

            // Ensure uniqueness
            while (await _repository.ExistsByCustomerCodeAsync(code))
            {
                var random = new Random();
                var suffix = random.Next(1000, 9999).ToString();
                code = $"CUS-{DateTime.Now:yyyyMMdd}-{suffix}";
            }

            return code;
        }
    }
}