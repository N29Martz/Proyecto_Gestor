using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestor_archivos_backend.Entities;

[Table("archivos")]
public class ArchivoEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("nombre")]
    public required string Nombre { get; set; }

    [Required]
    [Column("tamanio", TypeName = "decimal(10, 2)")]
    public decimal Tamanio { get; set; }

    [Required]
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; }

    [Required]
    [ForeignKey(nameof(TipoArchivoId))]
    [Column("tipo_archivo_id")]
    public Guid TipoArchivoId { get; set; }

    public TipoArchivoEntity? TipoArchivo { get; set; }

    [Required]
    [ForeignKey(nameof(CarpetaId))]
    [Column("carpeta_id")]
    public Guid CarpetaId { get; set; }

    public CarpetaEntity? Carpeta { get; set; }

    [Required]
    [Column("url_archivo")]
    public string UrlArchivo { get; set; }

    [Required]
    [Column("deleted")]
    public bool Deleted { get; set; } = false;


}
