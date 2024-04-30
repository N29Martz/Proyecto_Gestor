using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Users;

namespace gestor_archivos_backend.Services.Interfaces
{
    public interface IUsuariosService
    {
        Task<ResponseDto<UsuarioDto>> CreateUsuarioAsync(CreateUsuarioDto model);
        Task<ResponseDto<UsuarioDto>> DeleteUsuarioAsync(string id);
        Task<ResponseDto<UsuarioDto>> EditUsuarioAsync(EditUsuarioDto dto, string id);
        Task<ResponseDto<List<UsuarioDto>>> GetListAsync(string searchTerm = "");
        Task<ResponseDto<UsuarioDto>> ObtenerUsuarioPorId(string id);
    }
}
