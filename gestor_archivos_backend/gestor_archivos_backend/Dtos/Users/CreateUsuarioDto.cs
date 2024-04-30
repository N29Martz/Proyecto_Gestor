using gestor_archivos_backend.Dtos.Rol;

namespace gestor_archivos_backend.Dtos.Users
{
    public class CreateUsuarioDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        // public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<string> RolesId { get; set; } = [];
    
    }
}
