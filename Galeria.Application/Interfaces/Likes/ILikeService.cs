using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.DTO.Likes;
using Galeria.Domain.Entities.Likes;

namespace Galeria.Application.Interfaces.Likes
{
    public interface ILikeService: IServiceBase<Like, LikeDTO>
    {
    }
}
