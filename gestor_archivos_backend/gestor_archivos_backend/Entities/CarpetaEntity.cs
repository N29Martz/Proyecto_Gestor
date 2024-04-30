using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestor_archivos_backend.Entities;

[Table("carpetas")]
public class CarpetaEntity
{
    [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("nombre")]
    public required string Nombre { get; set; }

    [Required]
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; }

    [Required]
    [ForeignKey(nameof(UsuarioId))]
    [Column("usuario_id")]
    public string UsuarioId { get; set; }

    public UsuarioEntity? Usuario { get; set; }

    // [Required]
    [ForeignKey(nameof(CarpetaPadreId))]
    [Column("carpeta_padre_id")]
    public Guid? CarpetaPadreId { get; set; }

    public CarpetaEntity? CarpetaPadre { get; set; }

}
