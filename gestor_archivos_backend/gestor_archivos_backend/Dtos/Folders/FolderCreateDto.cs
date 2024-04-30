using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace gestor_archivos_backend.Dtos.Folders;

public class FolderCreateDto
{

    // [JsonIgnore]
    // public Guid Id { get; set; }

    [Required]
    [DisplayName("nombre")]
    [MaxLength(50, ErrorMessage = "La priopiedad {0} no puede tener más de 50 caracteres")]
    public required string Nombre { get; set; }

    [JsonIgnore]
    [DisplayName("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // [JsonIgnore]
    // [DisplayName("usuario_id")]
    // public string UsuarioId { get; set; }

    [DisplayName("carpeta_padre_id")]
    public Guid? CarpetaPadreId { get; set; }
    
}
