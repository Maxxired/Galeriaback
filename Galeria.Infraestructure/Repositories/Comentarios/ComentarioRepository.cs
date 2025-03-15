using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Infraestructure.Interfaces.Comentarios;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Repositories.Comentarios
{
    public class ComentarioRepository : BaseRepository<Comentario>, IComentarioRepository
    {
        private readonly ApplicationDbContext _context;
        public ComentarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
