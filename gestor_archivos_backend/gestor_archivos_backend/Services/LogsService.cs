
using AutoMapper;
using gestor_archivos_backend.DataBase;
using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Logs;
using gestor_archivos_backend.Entities.Logs;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestor_archivos_backend.Services;

public class LogsService : ILogsService
{

    public readonly GestorDbContext _contextGestor;

    public readonly LogDbContext _contextLog;

    private readonly HttpContext _httpContext;

    private readonly IMapper _mapper;

    private readonly string _USER_ID;

    public LogsService(
        GestorDbContext contextGestor, 
        LogDbContext contextLog, 
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    )
    {
        _contextGestor = contextGestor;
        _contextLog = contextLog;

        _mapper = mapper;

        _httpContext = httpContextAccessor.HttpContext;
        var idClaim = _httpContext.User.Claims.Where(x => x.Type == "userId").FirstOrDefault();
        _USER_ID = idClaim?.Value;

    }

    public async Task CreateLog(string userId, string type, Guid? fileId)
    {
        
        try
        {

            var existsUser = await _contextGestor.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("No se encontró al usuario");

            var logType = await _contextLog.LogTypes.FirstOrDefaultAsync(x => x.Description == type) ?? throw new Exception("No se encontro el tipo de log");

            if (fileId is not null)
            {
                
                var file = await _contextGestor.Archivos.FirstOrDefaultAsync(x => x.Id == fileId);

                if (file is null)
                    throw new Exception("No se encontro el archivo");

            }

            string description = $"El usuario con id { userId } realizo la acción de '{ type }'.";

            var log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Description = description,
                LogTypeId = logType.Id,
                FileId = fileId ?? null,
                UserId = userId
            };

            await _contextLog.AddAsync(log);
            await _contextLog.SaveChangesAsync();

            return;

        }
        catch (Exception e)
        {
            
            throw new Exception(e.Message);

        }

    }

    public async Task<ResponseDto<List<LogDto>>> GetAllLogs()
    {

        try
        {

            var logs = await _contextLog.Logs.Include(lt => lt.LogType).ToListAsync();

            if (logs.Count == 0)
                return new ResponseDto<List<LogDto>>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontraron logs",
                    Data = null
                };

            var logsDto = _mapper.Map<List<LogDto>>(logs);

            return new ResponseDto<List<LogDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de logs",
                Data = logsDto
            };

        }
        catch (Exception e)
        {
            
            return new ResponseDto<List<LogDto>>
            {
                StatusCode = 400,
                Status = false,
                Message = e.Message,
                Data = null
            };

        }

    }

    public async Task<ResponseDto<List<LogDto>>> GetLogsByUser()
    {

        try
        {

            var logs = await _contextLog.Logs.Where(l => l.UserId == _USER_ID).Include(lt => lt.LogType).ToListAsync();

            if (logs.Count == 0)
                return new ResponseDto<List<LogDto>>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontraron logs",
                    Data = null
                };

            // foreach (var log in logs)
            // {
            //     log.LogType = await _contextLog.LogTypes.FirstOrDefaultAsync(x => x.Id == log.LogTypeId);
            // }

            var logsDto = _mapper.Map<List<LogDto>>(logs);

            return new ResponseDto<List<LogDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Logs encontrados",
                Data = logsDto
            };

        }
        catch (Exception e)
        {
            
            return new ResponseDto<List<LogDto>>
            {
                StatusCode = 400,
                Status = false,
                Message = e.Message,
                Data = null
            };

        }
        
    }

};

public static class LogTypes
{
    
    public static string LoginUser { get { return "Inicio de sesión"; } }
    
    public static string LogoutUser { get { return "Cierre de sesión"; } }

    public static string RegisterUser { get { return "Registro de usuario"; } }

    public static string ModifyUser { get { return "Modificación de usuario"; } }

    public static string DeleteUser { get { return "Eliminación de usuario"; } }

    public static string CreateFile { get { return "Creación de archivo"; } }

    public static string ReadFiles { get { return "Lectura de archivos"; } }
    
    public static string DeleteFile { get { return "Eliminación de archivo"; } }

    public static string ModifyFile { get { return "Modificación de archivo"; } }

    public static string CreateFolder { get { return "Creación de carpeta"; } }

    public static string ReadFolder { get { return "Lectura de carpeta"; } }

    public static string DeleteFolder { get { return "Eliminación de carpeta"; } }

    public static string ModifyFolder { get { return "Modificación de carpeta"; } }

};
