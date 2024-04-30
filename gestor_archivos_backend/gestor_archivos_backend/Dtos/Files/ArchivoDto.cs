using gestor_archivos_backend.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestor_archivos_backend.Dtos.Files
{
    public class ArchivoDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        // recibir el archivo
        // public byte[] Archivo { get; set; }

        public string UrlArchivo { get; set; }

        // [Required]
        public decimal Tamanio { get; set; }

        // [Required]
        public DateTime FechaCreacion { get; set; }

        // [Required]
        public Guid TipoArchivoId { get; set; }

        // public TipoArchivoEntity TipoArchivo { get; set; } 

        // [Required]
        public Guid CarpetaId { get; set; }

        // public CarpetaEntity Carpeta { get; set; }
    }
}
