using System.ComponentModel.DataAnnotations;

namespace gestor_archivos_backend.Dtos.Archivos
{
    public class DescargaArchivoDto
    {
        //propiedades para la descarga de archivos ya que solo se necesita el id
        [Required]
        public Guid Id { get; set; }

    }
}
