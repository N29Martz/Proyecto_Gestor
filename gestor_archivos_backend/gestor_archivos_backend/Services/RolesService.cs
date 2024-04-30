using AutoMapper;
using gestor_archivos_backend.DataBase;
using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Rol;
using gestor_archivos_backend.Entities;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gestor_archivos_backend.Services
{
    public class RolesService : IRolesService
    {
        private readonly GestorDbContext _context;
        private readonly IMapper _mapper;

        public RolesService(GestorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //obtener todos los roles
        public async Task<ResponseDto<List<RolDto>>> GetListAsync(string searchTerm = "")
        {
            var rolesEntity = await _context.Roles.Where(r => r.Name.Contains(searchTerm)).ToListAsync();

            var rolesDto = _mapper.Map<List<RolDto>>(rolesEntity);

            return new ResponseDto<List<RolDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Datos obtenidos correctamente",
                Data = rolesDto
            };
        }

        //obtener un rol por id
        public async Task<ResponseDto<RolDto>> ObtenerRolPorId(string id)
        {
            var rolEntity = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (rolEntity is null)
            {
                return new ResponseDto<RolDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Rol no encontrado",
                };
            }

            var rolDto = _mapper.Map<RolDto>(rolEntity);

            return new ResponseDto<RolDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Rol encontrado",
                Data = rolDto
            };
        }

        //crear un rol
        public async Task<ResponseDto<RolDto>> CreateRolAsync(CreateRolDto model)
        {
            var rolEntity = _mapper.Map<RolEntity>(model);

            _context.Roles.Add(rolEntity);
            await _context.SaveChangesAsync();

            var rolDto = _mapper.Map<RolDto>(rolEntity);

            return new ResponseDto<RolDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Rol creado correctamente",
                Data = rolDto
            };
        }

        //editar un rol
        public async Task<ResponseDto<RolDto>> EditRolAsync(EditRolDto dto, string id)
        {
            var rolEntity = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (rolEntity is null)
            {
                return new ResponseDto<RolDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Rol no encontrado",
                };
            }

            _mapper.Map<EditRolDto, RolEntity>(dto, rolEntity);

            _context.Update(rolEntity);

            await _context.SaveChangesAsync();

            var rolDto = _mapper.Map<RolDto>(rolEntity);

            return new ResponseDto<RolDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Rol editado correctamente",
                Data = rolDto
            };
        }

        //eliminar un rol
        public async Task<ResponseDto<RolDto>> DeleteRolAsync(string id)
        {
            var rolEntity = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (rolEntity is null)
            {
                return new ResponseDto<RolDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Rol no encontrado",
                };
            }

            _context.Roles.Remove(rolEntity);

            await _context.SaveChangesAsync();


            return new ResponseDto<RolDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Rol eliminado correctamente"
            };
        }
    }
}
