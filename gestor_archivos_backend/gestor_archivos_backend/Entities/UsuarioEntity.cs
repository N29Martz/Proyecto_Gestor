using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestor_archivos_backend.Entities;

//TODO:sasgahgsa
//[Table("usuarios")]
public class UsuarioEntity : IdentityUser
{
    //[Key]
    // //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //[Column("id")]
    //public string Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("nombre")]
    public string Nombre { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("apellido")]
    public string Apellido { get; set; }

    //[Required]
    //[MaxLength(50)]
    //[Column("email")]
    //public required string Email { get; set; }

    //[Required]
    //[Column("contrasenia")]
    //public string Contrasenia { get; set; }

    [Required]
    [Column("fecha_registro")]
    public DateOnly FechaRegistro { get; set; }

}
