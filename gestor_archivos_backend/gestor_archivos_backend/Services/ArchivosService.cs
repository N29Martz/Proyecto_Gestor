using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using gestor_archivos_backend.DataBase;
using gestor_archivos_backend.Dtos;
using gestor_archivos_backend.Dtos.Archivos;
using gestor_archivos_backend.Dtos.Files;
using gestor_archivos_backend.Entities;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace gestor_archivos_backend.Services
{
    public class ArchivosService : IArchivosService
    {

        private readonly Cloudinary _cloudinary;

        //inyeccion de dependencias
        private readonly GestorDbContext _context;
        //para mapear las entidades con los dtos
        private readonly IMapper _mapper;
        //private readonly IGoogleService _googleService;
        //esta variable es para que me traiga el id del token
        private readonly HttpContext _httpContext;
        private readonly string _USER_ID;

        private readonly Dictionary<string, List<string>> _typeFiles = new () {
            { "Imagen", new List<string> { "jpg", "jpeg", "png", "gif", "bmp" } },
            { "Video", new List<string> { "mp4", "avi", "mov", "mkv", "flv" } },
            { "Audio", new List<string> { "mp3", "wav", "ogg", "flac" } },
            { "Documento", new List<string> { "pdf", "doc", "docx", "xls", "xlsx", "ppt", "pptx", "txt" } },
            { "Otro", new List<string> { "zip", "rar", "7z", "tar" } }
        };

        public ArchivosService(GestorDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
            var idClaim = _httpContext.User.Claims.Where(x => x.Type == "userId").FirstOrDefault();
            _USER_ID = idClaim?.Value;

            // _routeServer = Environment.GetEnvironmentVariable("ROUTE_SERVER") ?? Path.Combine(Directory.GetCurrentDirectory(), "files");

            _cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            _cloudinary.Api.Secure = true;

        }

        public async Task<ResponseDto<List<ArchivoDto>>> ArchivosUsuarioAsync() 
        {

            try
            {

                var archivosEntity = await _context.ArchivosUsuarios
                    .Where(a => a.UsuarioId == _USER_ID)
                    .Select(a => a.Archivo)
                    .ToListAsync();

                if (archivosEntity.Count == 0)
                    return new ResponseDto<List<ArchivoDto>>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No se encontraron archivos",
                        Data = null
                    };

                var archivosDto = _mapper.Map<List<ArchivoDto>>(archivosEntity);

                return new ResponseDto<List<ArchivoDto>>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Archivos obtenidos correctamente",
                    Data = archivosDto
                };

            }
            catch(Exception e)
            {
                return new ResponseDto<List<ArchivoDto>>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }

        }

        //metodo para mostar los archivos subidos
        public async Task<ResponseDto<List<ArchivoDto>>> ObtenerArchivosAsync(string searchTerm = "")
        {
            try
            {
                //como traer el USUARIOID?
                var archivosEntity = await _context.Archivos
                    .Where(a => a.Nombre.Contains(searchTerm)).ToListAsync();

                if (archivosEntity.Count == 0)
                    return new ResponseDto<List<ArchivoDto>>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No se encontraron archivos",
                        Data = null
                    };

                Console.WriteLine("This! " + archivosEntity);

                var archivosDto = _mapper.Map<List<ArchivoDto>>(archivosEntity);

                return new ResponseDto<List<ArchivoDto>>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Archivos obtenidos correctamente",
                    Data = archivosDto
                };

            }
            catch(Exception e)
            {
                return new ResponseDto<List<ArchivoDto>>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }
        }

        //metodo para obtener un archivo por su id
        public async Task<ResponseDto<ArchivoDto>> ObtenerArchivoPorIdAsync(Guid id)
        {
            try
            {
                var archivoEntity = await _context.Archivos.FirstOrDefaultAsync(t => t.Id == id);

                if (archivoEntity is null)
                {
                    return new ResponseDto<ArchivoDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Archivo no encontrado"
                    };
                }

                var archivoDto = _mapper.Map<ArchivoDto>(archivoEntity);

                return new ResponseDto<ArchivoDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Archivo obtenido correctamente",
                    Data = archivoDto
                };
            }
            catch(Exception e)
            {
                return new ResponseDto<ArchivoDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                };
            }
        }

        //metodo para subir un archivo
        public async Task<ResponseDto<ArchivoDto>> SubirArchivoAsync(CargaArchivoDto model)
        {
            
            try
            {

                var folder = model.CarpetaId != null ? await _context.Carpeta.FirstOrDefaultAsync(f => f.Id == model.CarpetaId) : null;

                if (folder is null)
                    return new ResponseDto<ArchivoDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Carpeta no encontrada."
                    };
                
                if (folder.UsuarioId != _USER_ID)
                    return new ResponseDto<ArchivoDto>
                    {
                        Status = false,
                        StatusCode = 403,
                        Message = "No tienes permisos para subir archivos a esta carpeta."
                    };

                // string nombre = model.Archivo.FileName.Replace(Path.GetExtension(model.Archivo.FileName), "");
                decimal tamanio = model.Archivo.Length / 1024;
                string extension = Path.GetExtension(model.Archivo.FileName);

                var typeFile = _typeFiles.FirstOrDefault(t => t.Value.Contains(extension.Replace(".", ""))).Key;
                var existsTypeFile = await _context.TipoArchivo.FirstOrDefaultAsync(t => t.Descripcion == typeFile) ?? null;
                
                if (existsTypeFile is null)
                    return new ResponseDto<ArchivoDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Tipo de archivo no encontrado."
                    };

                model.TipoArchivoId = existsTypeFile.Id;

                // preparar para subir a cloudinary
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(model.Archivo.FileName, model.Archivo.OpenReadStream()),
                    PublicId = $"{_USER_ID}/{model.CarpetaId}/{existsTypeFile.Descripcion}/{model.Nombre}",
                };

                var uploadResult = _cloudinary.Upload(uploadParams) ?? throw new Exception("No se pudo subir el archivo a cloudinary.");

                var archivoEntity = _mapper.Map<ArchivoEntity>(model);

                archivoEntity.Id = Guid.NewGuid();
                archivoEntity.UrlArchivo = uploadResult.Url.ToString();
                archivoEntity.Tamanio = tamanio;
                archivoEntity.Nombre = model.Nombre;
                archivoEntity.FechaCreacion = DateTime.Now;

                // Aquí debes obtener el usuario actualmente autenticado o el usuario al que deseas asociar el archivo
                // Puedes obtener el ID del usuario a partir del token o de alguna otra fuente
                ///string userId = ObtenerUserId(); // Implementa este método según cómo obtengas el ID del usuario

                var permissionId = (await _context.Permiso.FirstOrDefaultAsync(p => p.Descripcion == "Administrador")).Id;

                // Crear una nueva entidad ArchivosUsuariosEntity para asociar el archivo al usuario
                ArchivosUsuariosEntity archivosUsuariosEntity = new ()
                {
                    Archivo = archivoEntity,
                    UsuarioId = _USER_ID,
                    PermisoId = permissionId
                };

                // Agregar la entidad ArchivosUsuariosEntity a la tabla ArchivosUsuarios
                // await _context.ArchivosUsuarios.AddAsync(archivosUsuariosEntity);
                // await _context.SaveChangesAsync();

                await _context.ArchivosUsuarios.AddAsync(archivosUsuariosEntity);
                await _context.SaveChangesAsync();

                var archivoDto = _mapper.Map<ArchivoDto>(archivosUsuariosEntity.Archivo);

                return new ResponseDto<ArchivoDto>
                {
                    Status = true,
                    StatusCode = 201,
                    Message = "Archivo subido correctamente.",
                    Data = archivoDto
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<ArchivoDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }

        }

        //metodo para editar un archivo
        public async Task<ResponseDto<ArchivoDto>> EditarArchivoAsync(EditarArchivoDto dto, Guid id)
        {
            try
            {
                var archivoEntity = await _context.Archivos.FirstOrDefaultAsync(t => t.Id == id);

                if (archivoEntity is null)
                    return new ResponseDto<ArchivoDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Archivo no encontrado",
                    };

                var fileType = await _context.TipoArchivo.FirstOrDefaultAsync(t => t.Id == archivoEntity.TipoArchivoId);

                if (fileType is null)
                    return new ResponseDto<ArchivoDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Tipo de archivo no encontrado",
                    };

                var fileEdit = _mapper.Map<EditarArchivoDto, ArchivoEntity>(dto, archivoEntity);

                // _context.Archivos.Update(archivoEntity);

                // await _context.SaveChangesAsync();

                // /0a9566b5-9cd7-443e-9eeb-7ecc9e02acad/256edbe8-6a72-4a43-a574-20a2ca31f391/Imagen/Testing.jpg
                // actualizar el archivo en cloudinary
                var fromPublicId = $"{ _USER_ID }{ archivoEntity.UrlArchivo.Split($"/{ _USER_ID }").Last().Split(".").First() }";

                // throw new Exception(publicId);

                var toPublicId = $"{ _USER_ID }/{ archivoEntity.CarpetaId }/{ fileType.Descripcion }/{ dto.Nombre }";

                // var deleteParams = new DeletionParams(publicId);

                // var uploadParams = new ImageUploadParams()
                // {
                //     File = new FileDescription(fileEdit.Nombre, fileEdit.UrlArchivo),
                //     PublicId = $"{_USER_ID}/{dto.CarpetaId}/{dto.TipoArchivoId}/{dto.Nombre}",
                // };

                var renameParams = new RenameParams(fromPublicId, toPublicId) { Overwrite = true };

                var renameResult = _cloudinary.Rename(renameParams) ?? throw new Exception("No se pudo renombrar el archivo en cloudinary.");

                // var uploadResult = _cloudinary.Upload(uploadParams) ?? throw new Exception("No se pudo subir el archivo a cloudinary.");

                fileEdit.UrlArchivo = renameResult.Url.ToString();

                _context.Archivos.Update(fileEdit);
                await _context.SaveChangesAsync();

                var archivoDto = _mapper.Map<ArchivoDto>(fileEdit);

                return new ResponseDto<ArchivoDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Archivo editado correctamente",
                    Data = archivoDto
                };
                
            }
            catch(Exception e)
            {
                return new ResponseDto<ArchivoDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }
        }

        //metodo para eliminar un archivo
        public async Task<ResponseDto<ArchivoDto>> EliminarArchivoAsync(Guid id)
        {
            try
            {
                var archivoEntity = await _context.Archivos.FirstOrDefaultAsync(t => t.Id == id);

                if (archivoEntity is null)
                {
                    return new ResponseDto<ArchivoDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Archivo no encontrado",
                    };
                }

                // remover de cloudinary
                // var publicId = archivoEntity.UrlArchivo.Split("/").Last().Split(".").First();
                // var deleteParams = new DeletionParams(publicId);

                // _context.Archivos.Remove(archivoEntity);
                // await _context.SaveChangesAsync();

                // colocar el delete en true

                archivoEntity.Deleted = true;

                _context.Archivos.Update(archivoEntity);
                await _context.SaveChangesAsync();

                return new ResponseDto<ArchivoDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Archivo eliminado correctamente",
                };

            }
            catch(Exception e)
            {
                return new ResponseDto<ArchivoDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }
        }

        //metodo para la descarga de archivos
        public async Task<ResponseDto<ArchivoDto>> DescargarArchivoPorIdAsync(Guid id)
        {
            try
            {
                var archivoEntity = await _context.Archivos.FirstOrDefaultAsync(t => t.Id == id);

                if (archivoEntity is null)
                {
                    return new ResponseDto<ArchivoDto>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Archivo no encontrado"
                    };
                }

                var archivoDto = _mapper.Map<ArchivoDto>(archivoEntity);
                // Aquí podrías obtener la URL del archivo en la nube o en alguna ubicación de almacenamiento
                //string urlArchivo = archivoEntity.Ruta;

                return new ResponseDto<ArchivoDto>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "URL del archivo obtenida correctamente",
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<ArchivoDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }
        }

        //metodo para obtener la url del archivo en la nube FALTA IMPLEMENTAR
        //private async Task<string> GenerarUrlArchivo(string nombreArchivo)
        //{
        //    //no esta implementado
        //    return "https://ejemplo.com/" + nombreArchivo;
        //}
    }
}
