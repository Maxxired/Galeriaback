using System.ComponentModel.DataAnnotations.Schema;


namespace Galeria.Domain.DTO.Obras
{
    public class ObraCategoriaDTO : BaseDTO
    {
        public int IdObra { get; set; }
        public int IdCategoria { get; set; }
    }

}
