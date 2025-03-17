using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeria.Domain.DTO.Logs
{
    public class LogActionDTO : BaseDTO
    {
        public string Action { get; set; }
        public string Details { get; set; }
    }
}
