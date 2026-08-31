// Automotive.Assistance.API.Controllers/ServiceProviderUserController.cs
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotive.Assistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderUserController : ControllerBase
    {
        private readonly IServiceProviderUserService _service;
        private readonly ILogger<ServiceProviderUserController> _logger;

        public ServiceProviderUserController(
            IServiceProviderUserService service,
            ILogger<ServiceProviderUserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Get all service provider users
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetServiceProviderUsersList()
        {
            try
            {
                var response = await _service.GetServiceProviderUsersListAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all service provider users");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceProviderUserById(int id)
        {
            try
            {
                var response = await _service.GetServiceProviderUserByIdAsync(id);
                if (response == null)
                    return NotFound(new { error = $"Service provider user with ID {id} not found" });

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service provider user by ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get users by service provider ID
        /// </summary>
        [HttpGet("service-provider/{serviceProviderId}")]
        public async Task<IActionResult> GetUsersByServiceProviderId(int serviceProviderId)
        {
            try
            {
                var response = await _service.GetUsersByServiceProviderIdAsync(serviceProviderId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting users by service provider ID: {ServiceProviderId}", serviceProviderId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get service providers by user ID
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetServiceProvidersByUserId(int userId)
        {
            try
            {
                var response = await _service.GetServiceProvidersByUserIdAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service providers by user ID: {UserId}", userId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get primary contact for a service provider
        /// </summary>
        [HttpGet("primary-contact/{serviceProviderId}")]
        public async Task<IActionResult> GetPrimaryContact(int serviceProviderId)
        {
            try
            {
                var response = await _service.GetPrimaryContactAsync(serviceProviderId);
                if (response == null)
                    return NotFound(new { error = $"Primary contact not found for service provider ID {serviceProviderId}" });

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting primary contact for service provider ID: {ServiceProviderId}", serviceProviderId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new service provider user association
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateServiceProviderUser([FromBody] ServiceProviderUserCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _service.CreateServiceProviderUserAsync(dto);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating service provider user");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing service provider user
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceProviderUser(int id, [FromBody] ServiceProviderUserUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _service.UpdateServiceProviderUserAsync(id, dto);
                if (response == null)
                    return NotFound(new { error = $"Service provider user with ID {id} not found" });

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating service provider user with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a service provider user (hard delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceProviderUser(int id)
        {
            try
            {
                var result = await _service.DeleteServiceProviderUserAsync(id);
                if (!result)
                    return NotFound(new { error = $"Service provider user with ID {id} not found" });

                return Ok(new { message = "Service provider user deleted successfully", success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting service provider user with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update status (activate/deactivate) of a service provider user
        /// </summary>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] bool isActive)
        {
            try
            {
                var result = await _service.UpdateStatusAsync(id, isActive);
                if (!result)
                    return NotFound(new { error = $"Service provider user with ID {id} not found" });

                return Ok(new
                {
                    message = $"Service provider user {(isActive ? "activated" : "deactivated")} successfully",
                    success = true,
                    isActive = isActive
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status for service provider user with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update primary contact status of a service provider user
        /// </summary>
        [HttpPatch("{id}/primary-contact")]
        public async Task<IActionResult> UpdatePrimaryContact(int id, [FromQuery] bool isPrimaryContact)
        {
            try
            {
                var result = await _service.UpdatePrimaryContactAsync(id, isPrimaryContact);
                if (!result)
                    return NotFound(new { error = $"Service provider user with ID {id} not found" });

                return Ok(new
                {
                    message = $"Primary contact status updated successfully",
                    success = true,
                    isPrimaryContact = isPrimaryContact
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating primary contact for service provider user with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}