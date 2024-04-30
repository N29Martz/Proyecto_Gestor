using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace gestor_archivos_backend.Dtos.Folders;

public class FolderDto
{

    // [JsonIgnore]
    [Required]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("nombre")]
    [MaxLength(50, ErrorMessage = "La priopiedad {0} no puede tener más de 50 caracteres")]
    public required string Nombre { get; set; }

    [JsonIgnore]
    [Required]
    [DisplayName("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [JsonIgnore]
    [Required]
    [DisplayName("usuario_id")]
    public string UsuarioId { get; set; }

    // [Required]
    [DisplayName("carpeta_padre_id")]
    public Guid? CarpetaPadreId { get; set; }
    
}
