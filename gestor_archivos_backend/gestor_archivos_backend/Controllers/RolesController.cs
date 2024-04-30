using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Rol;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gestor_archivos_backend.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        // obtener todos los roles
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<RolDto>>>> GetListAsync(string searchTerm = "")
        {
             var rolesResponse = await _rolesService.GetListAsync(searchTerm);
        
             return StatusCode(rolesResponse.StatusCode, rolesResponse);
        }

        // obtener un rol por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<RolDto>>> ObtenerRolPorId(string id)
        {
            var rolResponse = await _rolesService.ObtenerRolPorId(id);

            return StatusCode(rolResponse.StatusCode, rolResponse);
        }

        // crear un rol
        [HttpPost]
        public async Task<ActionResult<ResponseDto<RolDto>>> CreateRolAsync([FromBody] CreateRolDto model)
        {
            var rolResponse = await _rolesService.CreateRolAsync(model);

            return StatusCode(rolResponse.StatusCode, rolResponse);
        }

        // editar un rol
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<RolDto>>> EditRolAsync([FromBody] EditRolDto dto, string id)
        {
            var rolResponse = await _rolesService.EditRolAsync(dto, id);

            return StatusCode(rolResponse.StatusCode, rolResponse);
        }

        // eliminar un rol
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<RolDto>>> DeleteRolAsync(string id)
        {
            var rolResponse = await _rolesService.DeleteRolAsync(id);

            return StatusCode(rolResponse.StatusCode, rolResponse);
        }
    }
}
