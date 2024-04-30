using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Users;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gestor_archivos_backend.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        // obtener todos los usuarios
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto<List<UsuarioDto>>>> GetListAsync(string searchTerm = "")
        {
            var usuariosResponse = await _usuariosService.GetListAsync(searchTerm);

            return StatusCode(usuariosResponse.StatusCode, usuariosResponse);
        }

        // obtener un usuario por id
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto<UsuarioDto>>> ObtenerUsuarioPorId(string id)
        {
            var usuarioResponse = await _usuariosService.ObtenerUsuarioPorId(id);

            return StatusCode(usuarioResponse.StatusCode, usuarioResponse);
        }

        // crear un usuario
        [HttpPost]
        // [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto<UsuarioDto>>> CreateUsuariosAsync([FromBody] CreateUsuarioDto model)
        {
            var usuarioResponse = await _usuariosService.CreateUsuarioAsync(model);

            return StatusCode(usuarioResponse.StatusCode, usuarioResponse);
        }

        //editar un usuario
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<ActionResult<ResponseDto<UsuarioDto>>> EditUsuarioAsync([FromBody] EditUsuarioDto dto, string id)
        {
            var usuarioResponse = await _usuariosService.EditUsuarioAsync(dto, id);

            return StatusCode(usuarioResponse.StatusCode, usuarioResponse);
        }

        //eliminar un usuario
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto<UsuarioDto>>> DeleteUsuarioAsync(string id)
        {
            var usuarioResponse = await _usuariosService.DeleteUsuarioAsync(id);

            return StatusCode(usuarioResponse.StatusCode, usuarioResponse);
        }
        

    }
}
