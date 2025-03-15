using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Interfaces.Obras;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Repositories.Obras
{
    public class ObraCategoriaRepository : BaseRepository<ObraCategoria>, IObraCategoriaRepository
    {
        private readonly ApplicationDbContext _context;
        public ObraCategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
