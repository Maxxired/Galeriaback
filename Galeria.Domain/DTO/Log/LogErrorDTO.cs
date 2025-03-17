using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeria.Domain.DTO.Logs
{
    public class LogErrorDTO : BaseDTO
    {
        public string Source { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
