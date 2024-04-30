using gestor_archivos_backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gestor_archivos_backend.DataBase
{
    //esto es para la conexion a la base de datos
    public class GestorDbContext : IdentityDbContext<UsuarioEntity, RolEntity, string>
    {
        public GestorDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.HasDefaultSchema("security");

            //ESTO ES PARA LA AUTENTICACION DE UN USUARIO
            //esto es para que las tablas de la base de datos se creen con el nombre que se le puso en el entity
            builder.Entity<UsuarioEntity>().ToTable("users");
            builder.Entity<RolEntity>().ToTable("roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");
            builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");

            //builder.Entity<RolEntity>().HasData(
            //new RolEntity
            //{
            //    Id = Guid.NewGuid(),
            //    Descripcion = "Lector"
            //},
            //new RolEntity
            //{
            //    Id = Guid.NewGuid(),
            //    Descripcion = "Editor"
            //},
            //new RolEntity
            //{
            //    Id = Guid.NewGuid(),
            //    Descripcion = "Administrador"
            //}
            //);

            builder.Entity<TipoArchivoEntity>().HasData(
                new TipoArchivoEntity { Id = Guid.NewGuid(), Descripcion = "Documento" },
                new TipoArchivoEntity { Id = Guid.NewGuid(), Descripcion = "Imagen" },
                new TipoArchivoEntity { Id = Guid.NewGuid(), Descripcion = "Video" },
                new TipoArchivoEntity { Id = Guid.NewGuid(), Descripcion = "Audio" },
                new TipoArchivoEntity { Id = Guid.NewGuid(), Descripcion = "Otro" }
            );

            builder.Entity<PermisoEntity>().HasData(
                new PermisoEntity { Id = Guid.NewGuid(), Descripcion = "Lectura" },
                new PermisoEntity { Id = Guid.NewGuid(), Descripcion = "Lectura y Escritura" },
                new PermisoEntity { Id = Guid.NewGuid(), Descripcion = "Administrador" }
            );

        }
        //todos los DbSet para las tablas de la base de datos
        //si no se registra aca no se crea la migracion
        public DbSet<ArchivoEntity> Archivos { get; set; }
        public DbSet<ArchivosUsuariosEntity> ArchivosUsuarios { get; set; }
        public DbSet<CarpetaEntity> Carpeta { get; set; }
        public DbSet<CarpetasCompartidasEntity> CarpetasCompartidas { get; set; }
        public DbSet<PermisoEntity> Permiso { get; set; }
        //public DbSet<RolEntity> Rol { get; set; }
        public DbSet<TipoArchivoEntity> TipoArchivo { get; set; }
        //public DbSet<UsuarioEntity> Usuario { get; set; }
        //public DbSet<UsuarioRolesEntity> UsuarioRoles { get; set; }
        public DbSet<VersionesArchivoEntity> VersionesArchivo { get; set; }


    }
}
