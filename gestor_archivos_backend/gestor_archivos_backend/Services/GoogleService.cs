using Google.Cloud.Storage.V1;
using Microsoft.IdentityModel.Tokens;
using gestor_archivos_backend.Services.Interfaces;
using gestor_archivos_backend.Dtos;


namespace gestor_archivos_backend.Services
{
    public class GoogleService : IGoogleService
    {
        private readonly GoogleConfigDto _configDto;

        public GoogleService(IConfiguration configuration)
        {
            _configDto = configuration.GetSection("GCPConfig").Get<GoogleConfigDto>() ??
                         throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> GuardarArchivo(IFormFile contenido, string contenedor)
        {
            if (contenido.FileName.IsNullOrEmpty()) return string.Empty;
            var contenidoArchivo = ReadFully(contenido);
            var client = StorageClient.Create();
            var extension = Path.GetExtension(contenido.FileName);
            var nombreArchivo = $"{contenedor}/{Guid.NewGuid()}";

            var obj = await client.UploadObjectAsync(_configDto.BucketName, nombreArchivo, extension, contenidoArchivo);
            return obj.Name;
        }

        private static Stream ReadFully(IFormFile input)
        {
            var ms = new MemoryStream();
            input.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public async Task BorrarArchivo(string ruta)
        {
            var client = StorageClient.Create();
            await client.DeleteObjectAsync(_configDto.BucketName, ruta);
        }

        public async Task<string> EditarArchivo(IFormFile contenido, string objName)
        {
            if (contenido.FileName.IsNullOrEmpty()) return string.Empty;
            var contenidoArchivo = ReadFully(contenido);
            var client = StorageClient.Create();
            var extension = Path.GetExtension(contenido.FileName);
            var obj = await client.UploadObjectAsync(_configDto.BucketName, objName, extension, contenidoArchivo);

            return obj.Name;
        }
    }
}
