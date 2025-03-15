using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Usuarios.Artistas;
using Galeria.Infraestructure.Interfaces.Usuarios.Artistas;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Repositories.Usuarios.Artistas
{
    public class ArtistaRepository : BaseRepository<Artista>, IArtistaRepository
    {
        private readonly ApplicationDbContext _context;
        public ArtistaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
