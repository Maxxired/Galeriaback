using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Likes;
using Galeria.Infraestructure.Interfaces.Likes;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Repositories.Likes
{
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        private readonly ApplicationDbContext _context;
        public LikeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
