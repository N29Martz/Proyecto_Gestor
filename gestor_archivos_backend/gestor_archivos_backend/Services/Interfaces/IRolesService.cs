using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Rol;

namespace gestor_archivos_backend.Services.Interfaces
{
    public interface IRolesService
    {
        Task<ResponseDto<RolDto>> CreateRolAsync(CreateRolDto model);
        Task<ResponseDto<RolDto>> DeleteRolAsync(string id);
        Task<ResponseDto<RolDto>> EditRolAsync(EditRolDto dto, string id);
        Task<ResponseDto<List<RolDto>>> GetListAsync(string searchTerm = "");
        Task<ResponseDto<RolDto>> ObtenerRolPorId(string id);
    }
}
