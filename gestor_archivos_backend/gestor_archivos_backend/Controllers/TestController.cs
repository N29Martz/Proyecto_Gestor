using gestor_archivos_backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestor_archivos_backend.Controllers;

[Route("api/test")]	
[Authorize(AuthenticationSchemes = "Bearer")]
public class TestController : ControllerBase
{

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    public ActionResult<ResponseDto<string>> Get()
    {
        return Ok(new ResponseDto<string>
        {
            Status = true,
            StatusCode = 200,
            Message = "Hello Admin!"
        });
    }	
    
}
