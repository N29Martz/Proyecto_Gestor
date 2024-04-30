using System.ComponentModel.DataAnnotations;

namespace gestor_archivos_backend.Dtos.Security
{
    public class LoginDto
    {
        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "el {0} es requerida")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es requerida")]
        public string Password { get; set; }
    }
}


