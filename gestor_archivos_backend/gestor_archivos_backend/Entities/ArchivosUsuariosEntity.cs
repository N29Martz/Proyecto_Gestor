using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestor_archivos_backend.Entities;

[Table("archivos_usuarios")]
public class ArchivosUsuariosEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    
    [Column("usuario_id")]
    public string UsuarioId { get; set; }
    [ForeignKey(nameof(UsuarioId))]
    public UsuarioEntity? Usuario { get; set; }

    [Required]
    [ForeignKey(nameof(ArchivoId))]
    [Column("archivo_id")]
    public Guid ArchivoId { get; set; }

    public ArchivoEntity? Archivo { get; set; }

    [Required]
    [ForeignKey(nameof(PermisoId))]
    [Column("permiso_id")]
    public Guid PermisoId { get; set; }

    public PermisoEntity? Permiso { get; set; }
};
