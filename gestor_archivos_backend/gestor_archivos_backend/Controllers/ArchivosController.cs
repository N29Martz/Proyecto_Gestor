using gestor_archivos_backend.DataBase;
using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Archivos;
using gestor_archivos_backend.Dtos.Files;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gestor_archivos_backend.Controllers
{
    [Route("api/archivos")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ArchivosController : ControllerBase
    {
        
        private readonly IArchivosService _archivosService;

        private readonly GestorDbContext _context;

        public ArchivosController(IArchivosService archivosService, GestorDbContext context)
        {
            _archivosService = archivosService;
            _context = context;
        }

        //metodo para mostar los archivos subidos
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto<List<ArchivoDto>>>> ObtenerArchivos(string searchTerm = "")
        {
            var archivosResponse = await _archivosService.ObtenerArchivosAsync(searchTerm);
        
            return StatusCode(archivosResponse.StatusCode, archivosResponse);
        }

        [HttpGet("user")]
        public async Task<ActionResult<ResponseDto<List<ArchivoDto>>>> GetFilesUser()
        {
            var archivosResponse = await _archivosService.ArchivosUsuarioAsync();
        
            return StatusCode(archivosResponse.StatusCode, archivosResponse);
        }

        //metodo para obtener un archivo por su id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ArchivoDto>>> ObtenerArchivoPorId(Guid id)
        {
            var archivoResponse = await _archivosService.ObtenerArchivoPorIdAsync(id);

            return StatusCode(archivoResponse.StatusCode, archivoResponse);
        }

        //metodo para subir un archivo
        [HttpPost]
        //el frombody es para que el archivo se envie en el cuerpo de la peticion
        public async Task<ActionResult<ResponseDto<ArchivoDto>>> SubirArchivo([FromForm] CargaArchivoDto model)
        {
            var archivoResponse = await _archivosService.SubirArchivoAsync(model);

            return StatusCode(archivoResponse.StatusCode, archivoResponse);
        }

        //metodo para editar un archivo
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ArchivoDto>>> EditarArchivo([FromBody] EditarArchivoDto dto, Guid id)
        {
            var archivoResponse = await _archivosService.EditarArchivoAsync(dto, id);

            return StatusCode(archivoResponse.StatusCode, archivoResponse);
        }

        //metodo para eliminar un archivo
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ArchivoDto>>> EliminarArchivo(Guid id)
        {
            var archivoResponse = await _archivosService.EliminarArchivoAsync(id);

            return StatusCode(archivoResponse.StatusCode, archivoResponse);
        }

        //metodo para descargar un archivo
        [HttpGet("descargar/{id}")]
        public async Task<ActionResult> DescargarArchivo(Guid id)
        {
            var archivoResponse = await _archivosService.DescargarArchivoPorIdAsync(id);

            if (archivoResponse.Status)
            {
                var archivoEntity = await _context.Archivos.FindAsync(id); // Obtener la entidad del archivo

                if (archivoEntity != null)
                {
                    //byte[] archivoBytes = ObtenerBytesDelArchivo(archivoEntity.Ruta);

                    //if (archivoBytes != null)
                    //{
                    //    return File(archivoBytes, "application/octet-stream", archivoEntity.Nombre);
                    //}
                    //else
                    //{
                    //    return StatusCode(500, "No se pudo obtener el archivo para descargar");
                    //}
                }
            }

            return StatusCode(archivoResponse.StatusCode, archivoResponse.Message);
        }

        // private byte[] ObtenerBytesDelArchivo(string rutaArchivo)
        // {
        //     try
        //     {
        //         // Leer el archivo y convertirlo a bytes
        //         byte[] archivoBytes;
        //         using (FileStream fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read))
        //         {
        //             archivoBytes = new byte[fileStream.Length];
        //             fileStream.Read(archivoBytes, 0, archivoBytes.Length);
        //         }
        //         return archivoBytes;
        //     }
        //     catch (Exception)
        //     {
        //         return null;
        //     }
        // }

    }
}
