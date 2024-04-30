using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace gestor_archivos_backend.Dtos.Archivos
{
    public class CargaArchivoDto
    {
        //propiedades para la carga de archivos

        [Required]
        public string Nombre { get; set; }

        // permitir que el usuario suba el archivo
        public IFormFile Archivo { get; set; }

        // [Required]
        [JsonIgnore]
        public decimal? Tamanio { get; set; }

        // [Required]
        [JsonIgnore]
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;

        // [Required]
        public Guid? TipoArchivoId { get; set; }

        // [Required]
        public Guid? CarpetaId { get; set; }

        // [Required]
        [JsonIgnore]
        public string? UrlArchivo { get; set; }

    }
}
