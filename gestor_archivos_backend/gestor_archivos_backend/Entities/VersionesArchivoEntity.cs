using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestor_archivos_backend.Entities;

[Table("versiones_archivo")]
public class VersionesArchivoEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(UsuarioId))]
    [Column("usuario_id")]
    public string UsuarioId { get; set; }

    public UsuarioEntity? Usuario { get; set; }


    [Required]
    [ForeignKey(nameof(ArchivoId))]
    [Column("archivo_id")]
    public Guid ArchivoId { get; set; }

    public ArchivoEntity? Archivo { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("descripcion")]
    public required string Descripcion { get; set; }

    [Required]
    [Column("fecha_edicion")]
    public DateTime FechaEdicion { get; set; }
}
