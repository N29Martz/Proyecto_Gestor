
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestor_archivos_backend.Controllers;

[ApiController]
[Route("api/logs")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class LogsController : ControllerBase
{
    
    private readonly ILogsService _logsService;

    public LogsController(ILogsService logsService)
    {
        _logsService = logsService;
    }

    [HttpGet("all")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetAllLogs()
    {
        
        var response = await _logsService.GetAllLogs();
        
        return StatusCode(
            response.StatusCode, 
            response
        );

    }

    [HttpGet]
    public async Task<IActionResult> GetLogsByUser()
    {
        
        var response = await _logsService.GetLogsByUser();
        
        return StatusCode(
            response.StatusCode, 
            response
        );

    }

}
