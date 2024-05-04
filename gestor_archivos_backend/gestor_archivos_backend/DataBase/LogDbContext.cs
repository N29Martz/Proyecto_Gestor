
using gestor_archivos_backend.Entities.Logs;
using gestor_archivos_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace gestor_archivos_backend.DataBase
{

    public class LogDbContext : DbContext
    { 

        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<LogTypeEntity>().HasData(
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Inicio de sesión",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Cierre de sesión",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Registro de usuario",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Modificación de usuario",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Eliminación de usuario",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Creación de archivo",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Lectura de archivos",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Eliminación de archivo",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Modificación de archivo",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Creación de carpeta",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Lectura de carpeta",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Eliminación de carpeta",
                },
                new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Description = "Modificación de carpeta",
                }
            );
        
        }

        public DbSet<LogEntity> Logs { get; set; }

        public DbSet<LogTypeEntity> LogTypes { get; set; }
        
    }

}
