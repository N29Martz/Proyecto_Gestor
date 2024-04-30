using gestor_archivos_backend.Entities;
using Microsoft.AspNetCore.Identity;


namespace gestor_archivos_backend.DataBase
{
    public static class GestorDbSeeder
    {
        public static async Task LoadDataAsync(
            UserManager<UsuarioEntity> userManager,
            RoleManager<RolEntity> roleManager,
            ILoggerFactory loggerFactory
            )
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new RolEntity { Name = "ADMIN", Descripcion = "Administrador" });
                    await roleManager.CreateAsync(new RolEntity { Name = "USER", Descripcion = "Usuario" });
                    await roleManager.CreateAsync(new RolEntity { Name = "VISOR", Descripcion = "Visor" });
                    await roleManager.CreateAsync(new RolEntity { Name = "EDITOR", Descripcion = "Editor" });
                }

                if (!userManager.Users.Any())
                {
                    var userAdmin = new UsuarioEntity
                    {
                        Email = "prueba@gmail.com",
                        UserName = "prueba@gmail.com",
                        Nombre = "Admin",
                        Apellido = "Admin",
                        // PasswordHash = "Prueba1234",
                        FechaRegistro = DateOnly.FromDateTime(DateTime.UtcNow)
                    };

                    await userManager.CreateAsync(userAdmin, "Temporak001*");

                    //asignar rol
                    await userManager.AddToRoleAsync(userAdmin, "ADMIN");

                    var normalUser = new UsuarioEntity
                    {
                        Email = "usernormal@gmail.com",
                        UserName = "usernormal@gmail.com",
                        Nombre = "Normal",
                        Apellido = "User",
                        // PasswordHash = "Temp1234",
                        FechaRegistro = DateOnly.FromDateTime(DateTime.UtcNow)
                    };

                    await userManager.CreateAsync(normalUser, "Temporak001*");
                    await userManager.AddToRoleAsync(normalUser, "USER");
                }

            }catch(Exception e)
            {
                var logger = loggerFactory.CreateLogger<GestorDbContext>();
                logger.LogError(e, "Error al enviar los datos ");
            }
        }
    }
}
