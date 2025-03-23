using Dapper;
using Galeria.Domain.Common.ViewModels.Artistas;
using Galeria.Domain.Common.ViewModels.Personas;
using Galeria.Domain.Entities.Usuarios.Artistas;
using Galeria.Infraestructure.Interfaces.Usuarios.Artistas;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Galeria.Infraestructure.Repositories.Usuarios.Artistas
{
    public class ArtistaRepository : BaseRepository<Artista>, IArtistaRepository
    {
        private readonly ApplicationDbContext _context;
        public ArtistaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> ActualizarPerfilArtista(string idApplicationUser, ActualizarPerfilArtistaVM datos)
        {
            string sql = @"UPDATE Tbl_Personas SET Nombres = @nombres, Apellidos = @apellidos, Edad = @edad, Pais = @pais, Biografia = @biografia
                        WHERE IdApplicationUser = @idApplicationUser AND IsDeleted = 0";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(sql, new
            {
                idApplicationUser,
                nombres = datos.Nombres,
                apellidos = datos.Apellidos,
                edad = datos.Edad,
                pais = datos.Pais,
                biografia = datos.Bigrafia
            });
            return result;

        }

        public async Task<int> SubirFotoPerfilArtista(string idApplicationUser, string url)
        {
            string sql = @"UPDATE AspNetUsers SET AvatarURL = @url WHERE IsDeleted = 0 AND Id = @idApplicationUser";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(sql, new { idApplicationUser, url });
            return result;
        }

        public async Task<int> ActualizarFotoPerfilArtista(string idApplicationUser, string url)
        {
            string sql = @"UPDATE AspNetUsers SET AvatarURL = @url WHERE IsDeleted = 0 AND Id = @idApplicationUser";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(sql, new { idApplicationUser, url });
            return result;
        }

        public async Task<int> EliminarFotoPerfilArtista(string idApplicationUser)
        {
            string sql = @"UPDATE AspNetUsers SET AvatarURL = NULL WHERE IsDeleted = 0 AND Id = @idApplicationUser";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(sql, new { idApplicationUser });
            return result;
        }
        public async Task<DatosPerfilArtistaVM> GetDatosPerfil(string idApplicationUser)
        {
            string sql = @"SELECT per.Nombres, per.Apellidos, per.Edad, usr.AvatarURL FROM Tbl_Personas AS per
                    INNER JOIN AspNetUsers AS usr ON usr.Id = per.IdApplicationUser AND usr.IsDeleted = 0
                    WHERE per.IdApplicationUser = @idApplicationuser AND per.IsDeleted = 0 ";

            var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<DatosPerfilArtistaVM>(sql, new { idApplicationUser });
            return result;
        }

    }
}
