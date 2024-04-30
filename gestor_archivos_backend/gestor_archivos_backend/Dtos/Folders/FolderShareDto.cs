namespace gestor_archivos_backend.Dtos.Folders;

public class FolderShareDto
{
    
    public string UsuarioId { get; set; }

    public Guid CarpetaId { get; set; }

    public Guid PermisoId { get; set; }
    
}
