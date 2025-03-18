using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Interfaces.Obras;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Galeria.Infraestructure.Repositories.Obras
{
    public class ObraEnExposicionRepository : BaseRepository<ObraEnExposicion>, IObraEnExposicionRepository
    {
        private readonly ApplicationDbContext _context;
        public ObraEnExposicionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> CrearRelacionAsync(int idObra, int idExposicion)
        {
            try
            {
                var relacion = new ObraEnExposicion { IdObra = idObra, IdExposicion = idExposicion };
                await _context.Set<ObraEnExposicion>().AddAsync(relacion);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> EliminarRelacionAsync(int idObra, int idExposicion)
        {
            try
            {
                var relacion = await _context.Set<ObraEnExposicion>()
                    .FirstOrDefaultAsync(o => o.IdObra == idObra && o.IdExposicion == idExposicion);

                if (relacion == null)
                    return false;

                _context.Set<ObraEnExposicion>().Remove(relacion);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Obra>> ListarObrasDeExposicionAsync(int idExposicion)
        {
            try
            {
                return await _context.Set<ObraEnExposicion>()
                    .Where(o => o.IdExposicion == idExposicion)
                    .Select(o => o.Obra)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Obra>();
            }
        }
    }
}
