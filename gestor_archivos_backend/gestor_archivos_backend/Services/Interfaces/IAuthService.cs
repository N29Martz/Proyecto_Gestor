using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Security;

namespace gestor_archivos_backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync();
    }
}
