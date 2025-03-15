using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Exposiciones;
using Galeria.Infraestructure.Interfaces.Esposiciones;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Repositories.Esposiciones
{
    public class ExposicionRepository : BaseRepository<Exposicion>, IExposicionRepository
    {
        private readonly ApplicationDbContext _context;
        public ExposicionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
