using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Files;
using gestor_archivos_backend.Dtos.Folders;

namespace gestor_archivos_backend.Services.Interfaces;

public interface IFoldersService
{

    Task<ResponseDto<FolderDto>> CreateAsync(FolderCreateDto dto);
    
    Task<ResponseDto<FolderDto>> GetByIdAsync(Guid id);

    Task<ResponseDto<List<FolderDto>>> GetByUserIdAsync(string userId);

    Task<ResponseDto<List<FolderDto>>> GetByUserAsync();
    
    Task<ResponseDto<List<FolderDto>>> UpdateAsync(FolderUpdateDto dto, Guid id);
    
    Task<ResponseDto<List<FolderDto>>> DeleteAsync(Guid id);

    Task<ResponseDto<FolderDto>> ShareAsync(FolderShareDto dto);

    Task<ResponseDto<List<ArchivoDto>>> GetFolderAndFiles(Guid folderId);

}
