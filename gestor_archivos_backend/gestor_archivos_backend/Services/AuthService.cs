using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using gestor_archivos_backend.Dtos.Security;
using gestor_archivos_backend.Entities;
using Microsoft.AspNetCore.Http;

namespace gestor_archivos_backend.Services
{
    
    public class AuthService : IAuthService         
    {
        
        private readonly SignInManager<UsuarioEntity> _signInManager;
        
        private readonly UserManager<UsuarioEntity> _userManager;
        
        private readonly IConfiguration _configuration;
        
        private readonly HttpContext _httpContext;
        
        private readonly string _USER_ID;

        public AuthService(
            SignInManager<UsuarioEntity> signInManager,
            UserManager<UsuarioEntity> userManager,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _httpContext = httpContextAccessor.HttpContext;
            var idClaim = _httpContext.User.Claims.Where(x => x.Type == "userId").FirstOrDefault();
            _USER_ID = idClaim?.Value;
        }
        
        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.Email,
                dto.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                // si todo sale bien se trabaja la respuesta con el token
                //para obtener el usario que inicio secion se usa el email
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);
                
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //se convierte a string el id del usuario
                    new Claim("userId", userEntity.Id)
                };

                var userRoles = await _userManager.GetRolesAsync(userEntity);

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Inicio de sesion exitoso",
                    Data = new LoginResponseDto
                    {
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        TokenExpiration = jwtToken.ValidTo
                    }
                };
            }

            //si todo sale mal se devuleve un error

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 400,
                Status = false,
                Message = "Inicio de sesion fallido",
                Data = null
            };
        }

        //este sera para refrescar el token
        public async Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync()
        {
            var userEntity = await _userManager.FindByIdAsync(_USER_ID);

            //vamos a generar un nuevo token con las cleims del usuario, cleims es la informacion que se guarda en el token
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", userEntity.Id)
                };

            var userRoles = await _userManager.GetRolesAsync(userEntity);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            //esto es para generar el token
            var jwtToken = GetToken(authClaims);

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Token renovado con exito",
                Data = new LoginResponseDto
                {
                    Email = userEntity.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    TokenExpiration = jwtToken.ValidTo
                }
            };
        }

        //metodo para generar el token
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

    }

}
