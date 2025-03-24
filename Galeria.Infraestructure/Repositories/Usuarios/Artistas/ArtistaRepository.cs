using Dapper;
using Galeria.Domain.Common.ViewModels.Artistas;
using Galeria.Domain.Common.ViewModels.Personas;
using Galeria.Domain.Entities;
using Galeria.Domain.Entities.Usuarios.Artistas;
using Galeria.Infraestructure.Interfaces.Usuarios.Artistas;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.AspNetCore.Identity;
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
                try
                {
                    string sqlPersona = @"
                    UPDATE Tbl_Artistas 
                    SET Nombres = @nombres, 
                        Apellidos = @apellidos, 
                        Edad = @edad, 
                        Pais = @pais, 
                        Biografia = @biografia
                    WHERE IdApplicationUser = @idApplicationUser AND IsDeleted = 0";

                    var parametros = new
                    {
                        idApplicationUser,
                        nombres = datos.Nombres,
                        apellidos = datos.Apellidos,
                        edad = datos.Edad,
                        pais = datos.Pais,
                        biografia = datos.Bigrafia
                    };

                    await _context.Database.GetDbConnection().ExecuteAsync(sqlPersona, parametros);

                    if (!string.IsNullOrEmpty(datos.Contraseña))
                    {
                        var passwordHasher = new PasswordHasher<ApplicationUser>();
                        string passwordHash = passwordHasher.HashPassword(null, datos.Contraseña);

                        string sqlUsuario = @"
                        UPDATE AspNetUsers 
                        SET PasswordHash = @passwordHash
                        WHERE Id = @idApplicationUser";

                        await _context.Database.GetDbConnection().ExecuteAsync(sqlUsuario, new { idApplicationUser, passwordHash });
                    }

                    return 1;
                }
                catch
                {
                    throw;
                }
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
