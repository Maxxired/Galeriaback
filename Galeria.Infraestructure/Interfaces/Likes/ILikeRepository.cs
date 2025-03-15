using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Likes;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Likes
{
    public interface ILikeRepository : IBaseRepository<Like>
    {
    }
}
