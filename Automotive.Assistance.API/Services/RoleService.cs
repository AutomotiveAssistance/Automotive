using Automotive.Assistance.API.Models.DTOs;
using Automotive.Assistance.API.Repositories;

namespace Automotive.Assistance.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<List<RoleResponseDTO>> GetRolesListAsync()
        {

            List<RoleResponseDTO> roles = new List<RoleResponseDTO>();

            var rolesResponse = await _roleRepository.GetRolesListAsync();


            if (rolesResponse.Any())
            {
                foreach (var role in rolesResponse)
                {
                    roles.Add(new RoleResponseDTO()
                    {
                        Id = role.Id,
                        Code = role.Code,
                        Name = role.Name,
                        Description = role.Description,
                        IsActive = role.IsActive

                    });

                }
            }
            return roles;
        }
    }
}
