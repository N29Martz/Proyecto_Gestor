using gestor_archivos_backend.Dtos.Rol;
using gestor_archivos_backend.Entities;

namespace gestor_archivos_backend.Dtos.Users
{
    public class UsuarioDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
