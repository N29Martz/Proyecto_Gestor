using AutoMapper;
using gestor_archivos_backend.DataBase;
using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Users;
using gestor_archivos_backend.Entities;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace gestor_archivos_backend.Services
{
    public class UsuariosService : IUsuariosService
    {
        //private readonly GestorDbContext _context;
        //se usa el UserManager para poder hacer operaciones con los usuarios
        // private readonly UserManager<UsuarioEntity> _context;
        private readonly UserManager<UsuarioEntity> _userManager;
        private readonly GestorDbContext _context;
        private readonly IMapper _mapper;

        // public UsuariosService(UserManager<UsuarioEntity> context, IMapper mapper)
        public UsuariosService(GestorDbContext context, UserManager<UsuarioEntity> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        //obtener todos los usuarios
        public async Task<ResponseDto<List<UsuarioDto>>> GetListAsync(string searchTerm = "")
        {
            var usuariosEntity = await _context.Users.Where(u => u.Nombre.Contains(searchTerm)).ToListAsync();

            var usuariosDto = _mapper.Map<List<UsuarioDto>>(usuariosEntity);

            return new ResponseDto<List<UsuarioDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Datos obtenidos correctamente",
                Data = usuariosDto
            };
        }

        //obtener un usuario por id
        public async Task<ResponseDto<UsuarioDto>> ObtenerUsuarioPorId(string id)
        {
            var usuarioEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (usuarioEntity is null)
            {
                return new ResponseDto<UsuarioDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Usuario no encontrado",
                };
            }

            var usuarioDto = _mapper.Map<UsuarioDto>(usuarioEntity);

            return new ResponseDto<UsuarioDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Datos obtenidos correctamente",
                Data = usuarioDto
            };
        }

        //crear un usuario
        public async Task<ResponseDto<UsuarioDto>> CreateUsuarioAsync(CreateUsuarioDto model)
        {
        
            try
            {

                var usuarioEntity = _mapper.Map<UsuarioEntity>(model);
                List<string> rolesNames = [];


                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                    return new ResponseDto<UsuarioDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "El email ya está en uso",
                    };

                // await _context.CreateAsync(usuarioEntity, model.Password);

                // verificar si los roles existen
                if (model.RolesId.Count > 0)
                    foreach (var roleId in model.RolesId)
                    {

                        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

                        if (role is null)
                            return new ResponseDto<UsuarioDto>
                            {
                                Status = false,
                                StatusCode = 404,
                                Message = "Rol no encontrado",
                            };

                        if (role.Name == "ADMIN")
                            return new ResponseDto<UsuarioDto>
                            {
                                Status = false,
                                StatusCode = 400,
                                Message = "Solo el administrador puede asignar el rol de ADMIN",
                            };

                        rolesNames.Add(role.Name);
                        
                    }

                if (rolesNames.Count == 0)
                    rolesNames.Add("USER");

                usuarioEntity.UserName = usuarioEntity.Email;
                usuarioEntity.FechaRegistro = DateOnly.FromDateTime(DateTime.Now);

                // Crear el usuario con los roles
                var result = await _userManager.CreateAsync(usuarioEntity, model.Password);

                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);

                // Asignar los roles al usuario
                await _userManager.AddToRolesAsync(usuarioEntity, rolesNames);

                var usuarioDto = _mapper.Map<UsuarioDto>(usuarioEntity);

                return new ResponseDto<UsuarioDto>
                {
                    Status = true,
                    StatusCode = 201,
                    Message = "Usuario creado correctamente",
                    Data = usuarioDto
                };
                
            }
            catch (Exception e)
            {
                
                return new ResponseDto<UsuarioDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = e.Message,
                };

            }

        }

        //editar un usuario
        public async Task<ResponseDto<UsuarioDto>> EditUsuarioAsync(EditUsuarioDto dto, string id)
        {
            var usuarioEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (usuarioEntity is null)
            {
                return new ResponseDto<UsuarioDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Usuario no encontrado",
                };
            }

            _mapper.Map<EditUsuarioDto, UsuarioEntity>(dto, usuarioEntity);

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                // Cambiar la contraseña del usuario
                var token = await _userManager.GeneratePasswordResetTokenAsync(usuarioEntity);
                var result = await _userManager.ResetPasswordAsync(usuarioEntity, token, dto.Password);

                if (!result.Succeeded)
                {
                    return new ResponseDto<UsuarioDto>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "Error al cambiar la contraseña",
                    };
                }
            }

            await _userManager.UpdateAsync(usuarioEntity);

            return new ResponseDto<UsuarioDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Usuario editado correctamente",
            };

            
        }

        //eliminar un usuario
        public async Task<ResponseDto<UsuarioDto>> DeleteUsuarioAsync(string id)
        {
            var usuarioEntity = await _userManager.FindByIdAsync(id);

            if (usuarioEntity == null)
            {
                return new ResponseDto<UsuarioDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Usuario no encontrado",
                };
            }

            var result = await _userManager.DeleteAsync(usuarioEntity);

            if (!result.Succeeded)
            {
                return new ResponseDto<UsuarioDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Error al eliminar el usuario",
                };
            }

            return new ResponseDto<UsuarioDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Usuario eliminado correctamente",
            };
        }
    }
}
