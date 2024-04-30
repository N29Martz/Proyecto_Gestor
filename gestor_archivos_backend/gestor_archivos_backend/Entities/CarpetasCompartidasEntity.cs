using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestor_archivos_backend.Entities;

[Table("carpetas_compartidas")]
public class CarpetasCompartidasEntity
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
    [ForeignKey(nameof(CarpetaId))]
    [Column("carpeta_id")]
    public Guid CarpetaId { get; set; }

    public CarpetaEntity? Carpeta { get; set; }

    [Required]
    [ForeignKey(nameof(PermisoId))]
    [Column("permiso_id")]
    public Guid PermisoId { get; set; }

    public PermisoEntity? Permiso { get; set; }
    
};
