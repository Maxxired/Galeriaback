using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Obras;

namespace Galeria.Domain.Entities.Categorias
{
    [Table("Tbl_Categorias")]
    public class Categoria : BaseEntity
    {
        public string Nombre { get; set; }
        public string DescripcionCategoria { get; set; }
        public string NombreCorto { get; set; }

        public virtual ICollection<ObraCategoria> ObrasCategorias { get; set; }
    }
}
