using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Categorias;

namespace Galeria.Domain.Entities.Obras
{
    [Table("Tbl_ObrasCategorias")]
    public class ObraCategoria
    {
        [ForeignKey("Obra")]
        public int IdObra { get; set; }
        public virtual Obra Obra { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
