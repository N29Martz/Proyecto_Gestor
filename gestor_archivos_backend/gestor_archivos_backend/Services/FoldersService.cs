using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using gestor_archivos_backend.DataBase;
using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Files;
using gestor_archivos_backend.Dtos.Folders;
using gestor_archivos_backend.Entities;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gestor_archivos_backend.Services;

public class FoldersService : IFoldersService
{

    private readonly GestorDbContext _context;

    private readonly IMapper _mapper;

    private readonly HttpContext _httpContext;

    private readonly string _USER_ID;

    public FoldersService(GestorDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {

        _context = context;
        _mapper = mapper;
        _httpContext = httpContextAccessor.HttpContext;

        var idClaim = _httpContext.User.Claims.Where(x => x.Type == "userId").FirstOrDefault();
        _USER_ID = idClaim?.Value;

    }

    public async Task<ResponseDto<FolderDto>> CreateAsync(FolderCreateDto dto)
    {

       try
       {

            var folderEntity = _mapper.Map<CarpetaEntity>(dto);

            folderEntity.UsuarioId = _USER_ID;
            folderEntity.Id = Guid.NewGuid();

            var folder = dto.CarpetaPadreId != null ? await _context.Carpeta.FirstOrDefaultAsync(f => f.Id == dto.CarpetaPadreId) : null;

            // if ((folderEntity.Id != folderEntity.CarpetaPadreId) && (folder is null)) 
            //     // asume que la carpeta padre es la raíz y le asigna el id de la carpeta raíz
            //     folderEntity.CarpetaPadreId = folderEntity.Id;

            var existsFolderName = await _context.Carpeta.AnyAsync(f => string.Equals(f.Nombre.ToLower(), folderEntity.Nombre.ToLower()) && f.UsuarioId == _USER_ID);

            if (existsFolderName)
                return new ResponseDto<FolderDto>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Ya existe una carpeta con ese nombre",
                };

            if (dto.CarpetaPadreId != null && folder is null)
                return new ResponseDto<FolderDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "La carpeta padre no existe",
                };
            
            if (folder != null)
                folderEntity.CarpetaPadreId = folder.Id;

            await _context.Carpeta.AddAsync(folderEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<FolderDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Carpeta creada con éxito",
                Data = _mapper.Map<FolderDto>(folderEntity),
            };
        
       }
       catch (Exception e)
       {
        
            return new ResponseDto<FolderDto>
            {
                Status = false,
                StatusCode = 500,
                Message = e.Message,
            };

       }

    }

    public async Task<ResponseDto<FolderDto>> GetByIdAsync(Guid id)
    {

        try
        {

            var folderEntity = await _context.Carpeta.FirstOrDefaultAsync(f => f.Id == id);

            if (folderEntity is null)
                return new ResponseDto<FolderDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "La carpeta no existe",
                };

            return new ResponseDto<FolderDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Carpeta encontrada",
                Data = _mapper.Map<FolderDto>(folderEntity),
            };

        }
        catch (Exception e)
        {

            return new ResponseDto<FolderDto>
            {
                Status = false,
                StatusCode = 500,
                Message = e.Message,
            };

        }

    }

    public async Task<ResponseDto<List<FolderDto>>> GetByUserIdAsync(string userId)
    {

        try
        {

            var foldersEntity = await _context.Carpeta.Where(f => f.UsuarioId == userId).ToListAsync();

            if (foldersEntity.Count == 0)
                return new ResponseDto<List<FolderDto>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No se encontraron carpetas",
                };

            return new ResponseDto<List<FolderDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Carpetas encontradas",
                Data = _mapper.Map<List<FolderDto>>(foldersEntity),
            };

        }
        catch (Exception e)
        {

            return new ResponseDto<List<FolderDto>>
            {
                Status = false,
                StatusCode = 500,
                Message = e.Message,
            };

        }

    }
    
    public async Task<ResponseDto<List<FolderDto>>> GetByUserAsync()
    {

        try
        {

            var foldersEntity = await _context.Carpeta.Where(f => f.UsuarioId == _USER_ID).ToListAsync();

            if (foldersEntity.Count == 0)
                return new ResponseDto<List<FolderDto>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No se encontraron carpetas",
                };

            return new ResponseDto<List<FolderDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Carpetas encontradas",
                Data = _mapper.Map<List<FolderDto>>(foldersEntity),
            };

        }
        catch (Exception e)
        {

            return new ResponseDto<List<FolderDto>>
            {
                Status = false,
                StatusCode = 500,
                Message = e.Message,
            };

        }

    }

    public async Task<ResponseDto<List<FolderDto>>> UpdateAsync(FolderUpdateDto dto, Guid id)
    {

        try
        {

            var folderEntity = await _context.Carpeta.FirstOrDefaultAsync(f => f.Id == id);

            if (folderEntity is null)
                return new ResponseDto<List<FolderDto>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "La carpeta no existe",
                };

            folderEntity.Nombre = dto.Nombre;

            _context.Carpeta.Update(folderEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<List<FolderDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Carpeta actualizada con éxito",
                Data = _mapper.Map<List<FolderDto>>(await _context.Carpeta.Where(f => f.UsuarioId == _USER_ID).ToListAsync()),
            };

        }
        catch (Exception e)
        {

            return new ResponseDto<List<FolderDto>>
            {
                Status = false,
                StatusCode = 500,
                Message = e.Message,
            };

        }

    }

    public async Task<ResponseDto<List<FolderDto>>> DeleteAsync(Guid id)
    {

        try
        {

            var folderEntity = await _context.Carpeta.FirstOrDefaultAsync(f => f.Id == id);

            if (folderEntity is null)
                return new ResponseDto<List<FolderDto>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "La carpeta no existe",
                };

            _context.Carpeta.Remove(folderEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<List<FolderDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Carpeta eliminada con éxito",
                Data = _mapper.Map<List<FolderDto>>(await _context.Carpeta.Where(f => f.UsuarioId == _USER_ID).ToListAsync()),
            };

        }
        catch (Exception e)
        {

            return new ResponseDto<List<FolderDto>>
            {
                Status = false,
                StatusCode = 500,
                Message = e.Message,
            };

        }

    }

    public async Task<ResponseDto<FolderDto>> ShareAsync(FolderShareDto dto)
    {

        try
        {

            var folderEntity = await _context.Carpeta.FirstOrDefaultAsync(f => f.Id == dto.CarpetaId);

            if (folderEntity is null)
                return new ResponseDto<FolderDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "La carpeta no existe",
                };

            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.UsuarioId);

            if (userEntity is null)
                return new ResponseDto<FolderDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "El usuario no existe",
                };

            var permisoEntity = await _context.Permiso.FirstOrDefaultAsync(p => p.Id == dto.PermisoId);

            if (permisoEntity is null)
                return new ResponseDto<FolderDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "El permiso no existe",
                };

            var carpetasCompartidasEntity = new CarpetasCompartidasEntity
            {
                CarpetaId = folderEntity.Id,
                UsuarioId = userEntity.Id,
                PermisoId = permisoEntity.Id,
            };

            await _context.CarpetasCompartidas.AddAsync(carpetasCompartidasEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<FolderDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Carpeta compartida con éxito",
                Data = _mapper.Map<FolderDto>(folderEntity),
            };

        }
        catch (Exception e)
        {

            return new ResponseDto<FolderDto>
            {
                Status = false,
                StatusCode = 500,
                Message = e.Message,
            };

        }

    }

    public async Task<ResponseDto<List<ArchivoDto>>> GetFolderAndFiles(Guid folderId)
    {

        try
        {

            var folderEntity = await _context.Carpeta.FirstOrDefaultAsync(f => f.Id == folderId);

            if (folderEntity is null)
                return new ResponseDto<List<ArchivoDto>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "La carpeta no existe",
                };

            var archivosEntity = await _context.Archivos.Where(a => a.CarpetaId == folderEntity.Id).ToListAsync();

            if (archivosEntity.Count == 0)
                return new ResponseDto<List<ArchivoDto>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No se encontraron archivos",
                };

            return new ResponseDto<List<ArchivoDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Archivos encontrados",
                Data = _mapper.Map<List<ArchivoDto>>(archivosEntity),
            };

        }
        catch (Exception e)
        {

            return new ResponseDto<List<ArchivoDto>>
            {
                Status = false,
                StatusCode = 500,
                Message = e.Message,
            };

        }

    } 
    
}
