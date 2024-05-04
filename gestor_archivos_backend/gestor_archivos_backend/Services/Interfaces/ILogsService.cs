
using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Logs;

namespace gestor_archivos_backend.Services.Interfaces;

public interface ILogsService
{


    Task CreateLog(string userId, string type, Guid? fileId = null);

    Task<ResponseDto<List<LogDto>>> GetAllLogs(); // solo para admin

    Task<ResponseDto<List<LogDto>>> GetLogsByUser();

    // Task<ResponseDto<LogDto>> GetLogById(Guid id);

}
