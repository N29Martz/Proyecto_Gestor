using AutoMapper;
using gestor_archivos_backend.Dtos.Archivos;
using gestor_archivos_backend.Dtos.Files;
using gestor_archivos_backend.Dtos.Folders;
using gestor_archivos_backend.Dtos.Rol;
using gestor_archivos_backend.Dtos.Users;
using gestor_archivos_backend.Entities;

namespace gestor_archivos_backend.Helpers
{
    public class AutoMapperProfile :  Profile
    {
        public AutoMapperProfile()
        {
            MapForArchivos();
            MapForFolders();
            MapForUsers();
            MapForRoles();
        }

        private void MapForArchivos()
        {
            CreateMap<ArchivoEntity, ArchivoDto>();
            CreateMap<CargaArchivoDto, ArchivoEntity>();
            CreateMap<EditarArchivoDto, ArchivoEntity>();
            CreateMap<ArchivoDto, ArchivoEntity>();
        }

        private void MapForFolders()
        {
            CreateMap<CarpetaEntity, FolderDto>();
            CreateMap<FolderCreateDto, CarpetaEntity>();
            CreateMap<FolderUpdateDto, CarpetaEntity>();
            CreateMap<FolderShareDto, CarpetasCompartidasEntity>();
            CreateMap<CarpetasCompartidasEntity, FolderDto>();
        }

        private void MapForUsers()
        {
            CreateMap<UsuarioEntity, UsuarioDto>();
            CreateMap<CreateUsuarioDto, UsuarioEntity>();
            CreateMap<EditUsuarioDto, UsuarioEntity>();
        }

        private void MapForRoles()
        {
            CreateMap<RolEntity, RolDto>();
            CreateMap<CreateRolDto, RolEntity>();
            CreateMap<EditRolDto, RolEntity>();
        }

    }
}
