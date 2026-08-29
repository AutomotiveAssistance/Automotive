using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Automotive.Assistance.API.Data;
using Automotive.Assistance.API.Models.Entities;
using Automotive.Assistance.API.Services;

namespace Automotive.Assistance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly ILogger<RolesController> _logger;

    public RolesController(IRoleService roleService, ILogger<RolesController> logger)
    {
        _roleService = roleService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetRolesListAsync()
    {
        try
        {
            var roles = await _roleService.GetRolesListAsync();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            _logger.LogError("Getting error " + ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}