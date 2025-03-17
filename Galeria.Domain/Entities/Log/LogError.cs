using System.ComponentModel.DataAnnotations.Schema;

namespace Galeria.Domain.Entities.Logs
{
    [Table("Tbl_LogError")]
    public class LogError : BaseEntity
    {
        public string Source { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
