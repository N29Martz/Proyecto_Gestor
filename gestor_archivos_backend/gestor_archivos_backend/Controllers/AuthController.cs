using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Security;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gestor_archivos_backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login(LoginDto dto)
        {
            var authResponse = await _authService.LoginAsync(dto);

            return StatusCode(authResponse.StatusCode, authResponse);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> RefreshToken()
        {
            var authResponse = await _authService.RefreshTokenAsync();

            return StatusCode(authResponse.StatusCode, authResponse);
        }
    }
}
