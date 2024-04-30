using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Files;
using gestor_archivos_backend.Dtos.Folders;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestor_archivos_backend.Controllers;

[ApiController]
[Route("api/folders")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class FoldersController : ControllerBase
{

    private readonly IFoldersService _foldersService;

    public FoldersController(IFoldersService foldersService)
    {
        _foldersService = foldersService;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Hello World!";
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto<FolderDto>>> CreateFolder([FromBody] FolderCreateDto dto) 
    {

        var folderResponse  = await _foldersService.CreateAsync(dto);

        return StatusCode(
            folderResponse.StatusCode,
            folderResponse
        );

    }

    [HttpGet("{id}")] // este es para traer una carpeta en especifico
    public async Task<ActionResult<ResponseDto<FolderDto>>> GetFolderById([FromRoute] Guid id)
    {
        
        var folderResponse = await _foldersService.GetByIdAsync(id);

        return StatusCode(
            folderResponse.StatusCode,
            folderResponse
        );

    }

    [HttpGet("user/{userId}")] // este es para que el usuario pueda ver sus carpetas
    public async Task<ActionResult<ResponseDto<List<FolderDto>>>> GetFoldersByUserId([FromRoute] string userId)
    {

        var folderResponse = await _foldersService.GetByUserIdAsync(userId);

        return StatusCode(
            folderResponse.StatusCode,
            folderResponse
        );

    } 
    
    [HttpGet("user")] // este es para que el usuario pueda ver sus carpetas
    public async Task<ActionResult<ResponseDto<List<FolderDto>>>> GetFoldersByUser()
    {

        var folderResponse = await _foldersService.GetByUserAsync();

        return StatusCode(
            folderResponse.StatusCode,
            folderResponse
        );

    }     

    [HttpPut("{id}")]
    // public async Task<ActionResult<ResponseDto<FolderDto>>> UpdateFolder([FromRoute] string id, [FromBody] FolderUpdateDto dto)
    public async Task<ActionResult<ResponseDto<FolderDto>>> UpdateFolder([FromBody] FolderUpdateDto dto, Guid id)
    {

        var folderResponse = await _foldersService.UpdateAsync(dto, id);

        return StatusCode(
            folderResponse.StatusCode,
            folderResponse
        );

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto<FolderDto>>> DeleteFolder([FromRoute] Guid id)
    {
        var folderResponse = await _foldersService.DeleteAsync(id);

        return StatusCode(
            folderResponse.StatusCode,
            folderResponse
        );
    }

    // Compartir carpeta
    [HttpPost("share")]
    public async Task<ActionResult<ResponseDto<FolderDto>>> ShareFolder([FromBody] FolderShareDto dto)
    {
        var folderResponse = await _foldersService.ShareAsync(dto);

        return StatusCode(
            folderResponse.StatusCode,
            folderResponse
        );
    }

    [HttpGet("files/{folderId}")]
    public async Task<ActionResult<ResponseDto<List<ArchivoDto>>>> GetFolderAndFiles([FromRoute] Guid folderId)
    {
        var folderResponse = await _foldersService.GetFolderAndFiles(folderId);

        return StatusCode(
            folderResponse.StatusCode,
            folderResponse
        );
    }
    
}
