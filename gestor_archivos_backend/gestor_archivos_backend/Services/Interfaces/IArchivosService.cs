using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Archivos;
using gestor_archivos_backend.Dtos.Files;

namespace gestor_archivos_backend.Services.Interfaces
{
    public interface IArchivosService
    {

        Task<ResponseDto<ArchivoDto>> DescargarArchivoPorIdAsync(Guid id);
        
        Task<ResponseDto<ArchivoDto>> EditarArchivoAsync(EditarArchivoDto dto, Guid id);
        
        Task<ResponseDto<ArchivoDto>> EliminarArchivoAsync(Guid id);
        
        Task<ResponseDto<ArchivoDto>> ObtenerArchivoPorIdAsync(Guid id);
        
        Task<ResponseDto<List<ArchivoDto>>> ObtenerArchivosAsync(string searchTerm = "");
        
        Task<ResponseDto<ArchivoDto>> SubirArchivoAsync(CargaArchivoDto cargaArchivoDto);

        Task<ResponseDto<List<ArchivoDto>>> ArchivosUsuarioAsync();

    }
    
}
