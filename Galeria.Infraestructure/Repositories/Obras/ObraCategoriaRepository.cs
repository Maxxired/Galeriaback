using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Interfaces.Obras;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace Galeria.Infraestructure.Repositories.Obras
{
    public class ObraCategoriaRepository : BaseRepository<ObraCategoria>, IObraCategoriaRepository
    {
        private readonly ApplicationDbContext _context;
        public ObraCategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task RemoveByObraIdAsync(int idObra)
        {
            var sql = "DELETE FROM Tbl_ObrasCategorias WHERE IdObra = @IdObra";
            await _context.Database.GetDbConnection().ExecuteAsync(sql, new { IdObra = idObra });
        }

        public async Task InsertManyAsync(List<ObraCategoria> categorias)
        {
            var sql = "INSERT INTO Tbl_ObrasCategorias (IdObra, IdCategoria) VALUES (@IdObra, @IdCategoria)";
            await _context.Database.GetDbConnection().ExecuteAsync(sql, categorias);
        }

    }
}
