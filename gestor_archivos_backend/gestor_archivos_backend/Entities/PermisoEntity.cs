using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestor_archivos_backend.Entities;

[Table("permisos")]
public class PermisoEntity
{
    [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("descripcion")]
    public required string Descripcion { get; set; }
}
