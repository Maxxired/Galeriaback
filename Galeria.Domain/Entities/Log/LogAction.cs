using System.ComponentModel.DataAnnotations.Schema;

namespace Galeria.Domain.Entities.Logs
{
    [Table("Tbl_LogAction")]
    public class LogAction : BaseEntity
    {
        public string Action { get; set; }
        public string Details { get; set; }
    }
}
