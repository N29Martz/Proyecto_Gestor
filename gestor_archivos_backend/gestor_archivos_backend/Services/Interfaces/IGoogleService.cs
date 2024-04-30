
namespace gestor_archivos_backend.Services.Interfaces
{
    public interface IGoogleService
    {
        Task BorrarArchivo(string ruta);
        Task<string> EditarArchivo(IFormFile contenido, string objName);
        Task<string> GuardarArchivo(IFormFile contenido, string contenedor);
    }
}
