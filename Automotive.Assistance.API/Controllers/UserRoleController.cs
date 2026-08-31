// Automotive.Assistance.API.Controllers/UserRoleController.cs
using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automotive.Assistance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(IUserRoleService userRoleService, ILogger<UserRoleController> logger)
        {
            _userRoleService = userRoleService;
            _logger = logger;
        }

        /// <summary>
        /// Get all user roles
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _userRoleService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all user roles");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all active user roles
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            try
            {
                var response = await _userRoleService.GetAllActiveAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active user roles");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get user role by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _userRoleService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user role by ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get user roles by user ID
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var response = await _userRoleService.GetByUserIdAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user roles by user ID: {UserId}", userId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get user roles by role ID
        /// </summary>
        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetByRoleId(int roleId)
        {
            try
            {
                var response = await _userRoleService.GetByRoleIdAsync(roleId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user roles by role ID: {RoleId}", roleId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Create a new user role
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRoleCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _userRoleService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = response.UserRoleId }, response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user role");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing user role
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserRoleUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _userRoleService.UpdateAsync(dto);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user role with ID: {Id}", dto.UserRoleId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a user role (hard delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _userRoleService.DeleteAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user role with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deactivate a user role (soft delete)
        /// </summary>
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                var response = await _userRoleService.SoftDeleteAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating user role with ID: {Id}", id);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Check if a user has a specific role
        /// </summary>
        [HttpGet("check")]
        public async Task<IActionResult> IsUserInRole([FromQuery] int userId, [FromQuery] int roleId)
        {
            try
            {
                var response = await _userRoleService.IsUserInRoleAsync(userId, roleId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user role for UserId: {UserId}, RoleId: {RoleId}", userId, roleId);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get paged user roles
        /// </summary>
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest("Page number and page size must be greater than 0");

            try
            {
                var response = await _userRoleService.GetPagedAsync(pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paged user roles");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}