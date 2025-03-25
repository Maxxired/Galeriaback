using Microsoft.AspNetCore.Identity;
using Galeria.Domain.Entities;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Categorias;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Domain.Entities.Exposiciones;
using Galeria.Domain.Entities.Likes;
using Galeria.Domain.Entities.Logs;
using Galeria.Domain.Entities.Obras;
using Galeria.Domain.Entities.Usuarios.Artistas;

namespace Galeria.Application.Services.Seeders
{
    public class Seed
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<string>> SeedDataAsync()
        {
            var messages = new List<string>();

            await CreateRolesAsync(messages);
            await SeedUsersAndPersonasAsync(messages);
            await SeedArtistasAsync(messages);
            await SeedCategoriasAsync(messages);
            await SeedObrasAsync(messages);
            await SeedObrasCategoriasAsync(messages);
            await SeedExposicionesAsync(messages);
            await SeedObrasEnExposicionAsync(messages);
            await SeedComentariosAsync(messages);
            await SeedLikesAsync(messages);
            await SeedLogActionsAsync(messages);
            await SeedLogErrorsAsync(messages);

            return messages;
        }

        private async Task CreateRolesAsync(List<string> messages)
        {
            string[] roleNames = { "Admin", "Usuario", "Artista" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                    messages.Add($"Rol {roleName} creado exitosamente.");
                }
                else
                {
                    messages.Add($"El rol {roleName} ya existe.");
                }
            }
        }

        private async Task SeedUsersAndPersonasAsync(List<string> messages)
        {
            if (!_context.Users.Any())
            {
                var users = new[]
                {
                    new ApplicationUser { UserName = "root@admin.com", Email = "root@admin.com", IsDeleted = false },
                    new ApplicationUser { UserName = "socio@test.com", Email = "socio@test.com", IsDeleted = false },
                    new ApplicationUser { UserName = "cliente@test.com", Email = "cliente@test.com", IsDeleted = false },
                    new ApplicationUser { UserName = "juan@gmail.com", Email = "juan@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "alonso@gmail.com", Email = "alonso@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "andres@gmail.com", Email = "andres@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "diego@gmail.com", Email = "diego@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "merida@gmail.com", Email = "merida@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "jaime@gmail.com", Email = "jaime@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "alison@gmail.com", Email = "alison@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "cochi@gmail.com", Email = "cochi@gmail.com" , IsDeleted = false },
                    new ApplicationUser { UserName = "david@gmail.com", Email = "davida@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "erika@gmail.com", Email = "erika@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "ocelot@gmail.com", Email = "ocelot@gmail.com", IsDeleted = false },
                    // Artistas
                    new ApplicationUser { UserName = "picasso@art.com", Email = "picasso@art.com", IsDeleted = false },
                    new ApplicationUser { UserName = "vangogh@art.com", Email = "vangogh@art.com", IsDeleted = false },
                    new ApplicationUser { UserName = "dali@art.com", Email = "dali@art.com", IsDeleted = false },
                    new ApplicationUser { UserName = "monet@art.com", Email = "monet@art.com", IsDeleted = false },
                    new ApplicationUser { UserName = "klimt@art.com", Email = "klimt@art.com", IsDeleted = false }
                };

                foreach (var user in users)
                {
                    var result = await _userManager.CreateAsync(user, "Password.123");
                    if (result.Succeeded)
                    {
                        messages.Add($"Usuario {user.UserName} creado exitosamente.");

                        if (user.UserName == "root@admin.com")
                            await _userManager.AddToRoleAsync(user, "Admin");
                        else if (user.UserName.EndsWith("@art.com"))
                            await _userManager.AddToRoleAsync(user, "Artista");
                        else
                            await _userManager.AddToRoleAsync(user, "Usuario");
                    }
                    else
                    {
                        messages.Add($"Error al crear el usuario {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            if (!_context.Personas.Any())
            {
                var personas = new[]
                {
                    new Persona { Nombres = "John", Apellidos = "Doe", Edad = 30, IdApplicationUser = _context.Users.First(u => u.UserName == "root@admin.com").Id },
                    new Persona { Nombres = "Sarah", Apellidos = "Lee", Edad = 28, IdApplicationUser = _context.Users.First(u => u.UserName == "socio@test.com").Id },
                    new Persona { Nombres = "Mark", Apellidos = "Parker", Edad = 25, IdApplicationUser = _context.Users.First(u => u.UserName == "cliente@test.com").Id },
                    new Persona { Nombres = "Juan", Apellidos = "Perez", Edad = 35, IdApplicationUser = _context.Users.First(u => u.UserName == "juan@gmail.com").Id },
                    new Persona { Nombres = "Alonso", Apellidos = "Garcia", Edad = 40, IdApplicationUser = _context.Users.First(u => u.UserName == "alonso@gmail.com").Id },
                    new Persona { Nombres = "Andres", Apellidos = "Hernandez", Edad = 45, IdApplicationUser = _context.Users.First(u => u.UserName == "andres@gmail.com").Id },
                    new Persona { Nombres = "Diego", Apellidos = "Jimenez", Edad = 50, IdApplicationUser = _context.Users.First(u => u.UserName == "diego@gmail.com").Id },
                    new Persona { Nombres = "Merida", Apellidos = "Lopez", Edad = 55, IdApplicationUser = _context.Users.First(u => u.UserName == "merida@gmail.com").Id },
                    new Persona { Nombres = "Jaime", Apellidos = "Gutierrez", Edad = 60, IdApplicationUser = _context.Users.First(u => u.UserName == "jaime@gmail.com").Id },
                    new Persona { Nombres = "Alison", Apellidos = "Gomez", Edad = 65, IdApplicationUser = _context.Users.First(u => u.UserName == "alison@gmail.com").Id },
                    new Persona { Nombres = "Cochi", Apellidos = "Gutierrez", Edad = 70, IdApplicationUser = _context.Users.First(u => u.UserName == "cochi@gmail.com").Id },
                    new Persona { Nombres = "David", Apellidos = "Gomez", Edad = 75, IdApplicationUser = _context.Users.First(u => u.UserName == "david@gmail.com").Id },
                    new Persona { Nombres = "Erika", Apellidos = "Gomez", Edad = 80, IdApplicationUser = _context.Users.First(u => u.UserName == "erika@gmail.com").Id },
                    new Persona { Nombres = "Ocelot", Apellidos = "Gomez", Edad = 85, IdApplicationUser = _context.Users.First(u => u.UserName == "ocelot@gmail.com").Id }
                };

                _context.Personas.AddRange(personas);
                await _context.SaveChangesAsync();
                messages.Add("Personas creadas exitosamente.");
            }
        }

        private async Task SeedArtistasAsync(List<string> messages)
        {
            if (!_context.Artistas.Any())
            {
                var artistas = new[]
                {
                    new Artista {
                        Nombres = "Pablo",
                        Apellidos = "Picasso",
                        Edad = 91,
                        Pais = "España",
                        Biografia = "Pintor y escultor español, creador del cubismo.",
                        FechaNacimiento = new DateTime(1881, 10, 25),
                        IdApplicationUser = _context.Users.First(u => u.UserName == "picasso@art.com").Id
                    },
                    new Artista {
                        Nombres = "Vincent",
                        Apellidos = "van Gogh",
                        Edad = 37,
                        Pais = "Países Bajos",
                        Biografia = "Pintor postimpresionista holandés conocido por sus paisajes y autorretratos.",
                        FechaNacimiento = new DateTime(1853, 3, 30),
                        IdApplicationUser = _context.Users.First(u => u.UserName == "vangogh@art.com").Id
                    },
                    new Artista {
                        Nombres = "Salvador",
                        Apellidos = "Dalí",
                        Edad = 84,
                        Pais = "España",
                        Biografia = "Artista surrealista español conocido por sus imágenes oníricas.",
                        FechaNacimiento = new DateTime(1904, 5, 11),
                        IdApplicationUser = _context.Users.First(u => u.UserName == "dali@art.com").Id
                    },
                    new Artista {
                        Nombres = "Claude",
                        Apellidos = "Monet",
                        Edad = 86,
                        Pais = "Francia",
                        Biografia = "Pintor francés, figura clave del impresionismo.",
                        FechaNacimiento = new DateTime(1840, 11, 14),
                        IdApplicationUser = _context.Users.First(u => u.UserName == "monet@art.com").Id
                    },
                    new Artista {
                        Nombres = "Gustav",
                        Apellidos = "Klimt",
                        Edad = 55,
                        Pais = "Austria",
                        Biografia = "Pintor simbolista austriaco, conocido por su estilo decorativo.",
                        FechaNacimiento = new DateTime(1862, 7, 14),
                        IdApplicationUser = _context.Users.First(u => u.UserName == "klimt@art.com").Id
                    }
                };

                _context.Artistas.AddRange(artistas);
                await _context.SaveChangesAsync();
                messages.Add("Artistas creados exitosamente.");
            }
        }

        private async Task SeedCategoriasAsync(List<string> messages)
        {
            if (!_context.Categorias.Any())
            {
                var categorias = new[]
                {
                    new Categoria {
                        Nombre = "Pintura al óleo",
                        DescripcionCategoria = "Obras creadas con pintura al óleo sobre lienzo.",
                        NombreCorto = "Óleo"
                    },
                    new Categoria {
                        Nombre = "Acuarela",
                        DescripcionCategoria = "Obras creadas con acuarelas sobre papel especial.",
                        NombreCorto = "Acuarela"
                    },
                    new Categoria {
                        Nombre = "Escultura",
                        DescripcionCategoria = "Obras tridimensionales creadas con diversos materiales.",
                        NombreCorto = "Escultura"
                    },
                    new Categoria {
                        Nombre = "Fotografía artística",
                        DescripcionCategoria = "Fotografías con valor artístico y composición estudiada.",
                        NombreCorto = "Fotografía"
                    },
                    new Categoria {
                        Nombre = "Arte digital",
                        DescripcionCategoria = "Obras creadas mediante herramientas digitales.",
                        NombreCorto = "Digital"
                    }
                };

                _context.Categorias.AddRange(categorias);
                await _context.SaveChangesAsync();
                messages.Add("Categorías creadas exitosamente.");
            }
        }

        private async Task SeedObrasAsync(List<string> messages)
        {
            if (!_context.Obras.Any())
            {
                var picassoId = _context.Artistas.First(a => a.Nombres == "Pablo").Id;
                var vangoghId = _context.Artistas.First(a => a.Nombres == "Vincent").Id;
                var daliId = _context.Artistas.First(a => a.Nombres == "Salvador").Id;
                var monetId = _context.Artistas.First(a => a.Nombres == "Claude").Id;
                var klimtId = _context.Artistas.First(a => a.Nombres == "Gustav").Id;

                var obras = new[]
                {
                    new Obra {
                        Titulo = "Guernica",
                        Descripcion = "Representación del bombardeo de Guernica durante la guerra civil española.",
                        IdArtista = picassoId,
                        Precio = 15000000m,
                        ImagenUrl = "Uploads\\FotosObras\\imagen.jpg",
                        Slug = "guernica-picasso"
                    },
                    new Obra {
                        Titulo = "La noche estrellada",
                        Descripcion = "Paisaje nocturno con remolinos característicos del estilo de van Gogh.",
                        IdArtista = vangoghId,
                        Precio = 8000000m,
                        ImagenUrl = "Uploads\\FotosObras\\imagen.jpg",
                        Slug = "noche-estrellada-vangogh"
                    },
                    new Obra {
                        Titulo = "La persistencia de la memoria",
                        Descripcion = "Famosos relojes derretidos que representan la fluidez del tiempo.",
                        IdArtista = daliId,
                        Precio = 5000000m,
                        ImagenUrl = "Uploads\\FotosObras\\imagen.jpg",
                        Slug = "persistencia-memoria-dali"
                    },
                    new Obra {
                        Titulo = "Nenúfares",
                        Descripcion = "Serie de pinturas que representan el estanque de nenúfares en su jardín.",
                        IdArtista = monetId,
                        Precio = 4000000m,
                        ImagenUrl = "Uploads\\FotosObras\\imagen.jpg",
                        Slug = "nenufares-monet"
                    },
                    new Obra {
                        Titulo = "El beso",
                        Descripcion = "Representación de una pareja abrazada con un estilo dorado característico.",
                        IdArtista = klimtId,
                        Precio = 6000000m,
                        ImagenUrl = "Uploads\\FotosObras\\imagen.jpg",
                        Slug = "beso-klimt"
                    },
                    new Obra {
                        Titulo = "Las señoritas de Avignon",
                        Descripcion = "Obra pionera del cubismo que representa figuras femeninas.",
                        IdArtista = picassoId,
                        Precio = 12000000m,
                        ImagenUrl = "Uploads\\FotosObras\\imagen.jpg",
                        Slug = "senoritas-avignon-picasso"
                    },
                    new Obra {
                        Titulo = "Los girasoles",
                        Descripcion = "Uploads\\FotosObras\\imagen.jpg",
                        IdArtista = vangoghId,
                        Precio = 7500000m,
                        ImagenUrl = "/images/girasoles.jpg",
                        Slug = "girasoles-vangogh"
                    }
                };

                _context.Obras.AddRange(obras);
                await _context.SaveChangesAsync();
                messages.Add("Obras creadas exitosamente.");
            }
        }

        private async Task SeedObrasCategoriasAsync(List<string> messages)
        {
            if (!_context.ObrasCategorias.Any())
            {
                var pinturaId = _context.Categorias.First(c => c.Nombre == "Pintura al óleo").Id;
                var acuarelaId = _context.Categorias.First(c => c.Nombre == "Acuarela").Id;
                var esculturaId = _context.Categorias.First(c => c.Nombre == "Escultura").Id;
                var fotoId = _context.Categorias.First(c => c.Nombre == "Fotografía artística").Id;
                var digitalId = _context.Categorias.First(c => c.Nombre == "Arte digital").Id;

                var guernicaId = _context.Obras.First(o => o.Titulo == "Guernica").Id;
                var nocheEstrelladaId = _context.Obras.First(o => o.Titulo == "La noche estrellada").Id;
                var persistenciaId = _context.Obras.First(o => o.Titulo == "La persistencia de la memoria").Id;
                var nenufaresId = _context.Obras.First(o => o.Titulo == "Nenúfares").Id;
                var besoId = _context.Obras.First(o => o.Titulo == "El beso").Id;
                var senoritasId = _context.Obras.First(o => o.Titulo == "Las señoritas de Avignon").Id;
                var girasolesId = _context.Obras.First(o => o.Titulo == "Los girasoles").Id;

                var obrasCategorias = new[]
                {
                    new ObraCategoria { IdObra = guernicaId, IdCategoria = pinturaId },
                    new ObraCategoria { IdObra = nocheEstrelladaId, IdCategoria = pinturaId },
                    new ObraCategoria { IdObra = persistenciaId, IdCategoria = pinturaId },
                    new ObraCategoria { IdObra = nenufaresId, IdCategoria = acuarelaId },
                    new ObraCategoria { IdObra = besoId, IdCategoria = pinturaId },
                    new ObraCategoria { IdObra = senoritasId, IdCategoria = pinturaId },
                    new ObraCategoria { IdObra = girasolesId, IdCategoria = pinturaId },
                    new ObraCategoria { IdObra = nenufaresId, IdCategoria = pinturaId },
                    new ObraCategoria { IdObra = besoId, IdCategoria = digitalId }
                };

                _context.ObrasCategorias.AddRange(obrasCategorias);
                await _context.SaveChangesAsync();
                messages.Add("Relaciones Obra-Categoría creadas exitosamente.");
            }
        }

        private async Task SeedExposicionesAsync(List<string> messages)
        {
            if (!_context.Exposiciones.Any())
            {
                var picassoId = _context.Artistas.First(a => a.Nombres == "Pablo").Id;
                var vangoghId = _context.Artistas.First(a => a.Nombres == "Vincent").Id;
                var daliId = _context.Artistas.First(a => a.Nombres == "Salvador").Id;
                var monetId = _context.Artistas.First(a => a.Nombres == "Claude").Id;
                var klimtId = _context.Artistas.First(a => a.Nombres == "Gustav").Id;

                var exposiciones = new[]
                {
                    new Exposicion {
                        Nombre = "Cubismo y más allá",
                        FechaInicio = new DateTime(2023, 5, 15),
                        FechaFin = new DateTime(2023, 8, 20),
                        IdArtista = picassoId
                    },
                    new Exposicion {
                        Nombre = "Postimpresionismo: Colores y emociones",
                        FechaInicio = new DateTime(2023, 6, 1),
                        FechaFin = new DateTime(2023, 9, 30),
                        IdArtista = vangoghId
                    },
                    new Exposicion {
                        Nombre = "Surrealismo: Más allá de la realidad",
                        FechaInicio = new DateTime(2023, 7, 10),
                        FechaFin = new DateTime(2023, 10, 15),
                        IdArtista = daliId
                    },
                    new Exposicion {
                        Nombre = "Impresionismo: La luz capturada",
                        FechaInicio = new DateTime(2023, 8, 5),
                        FechaFin = new DateTime(2023, 11, 10),
                        IdArtista = monetId
                    },
                    new Exposicion {
                        Nombre = "Simbolismo y arte decorativo",
                        FechaInicio = new DateTime(2023, 9, 1),
                        FechaFin = new DateTime(2023, 12, 31),
                        IdArtista = klimtId
                    }
                };

                _context.Exposiciones.AddRange(exposiciones);
                await _context.SaveChangesAsync();
                messages.Add("Exposiciones creadas exitosamente.");
            }
        }

        private async Task SeedObrasEnExposicionAsync(List<string> messages)
        {
            if (!_context.ObrasEnExposicion.Any())
            {
                var cubismoId = _context.Exposiciones.First(e => e.Nombre == "Cubismo y más allá").Id;
                var postimpresionismoId = _context.Exposiciones.First(e => e.Nombre == "Postimpresionismo: Colores y emociones").Id;
                var surrealismoId = _context.Exposiciones.First(e => e.Nombre == "Surrealismo: Más allá de la realidad").Id;
                var impresionismoId = _context.Exposiciones.First(e => e.Nombre == "Impresionismo: La luz capturada").Id;
                var simbolismoId = _context.Exposiciones.First(e => e.Nombre == "Simbolismo y arte decorativo").Id;

                var guernicaId = _context.Obras.First(o => o.Titulo == "Guernica").Id;
                var senoritasId = _context.Obras.First(o => o.Titulo == "Las señoritas de Avignon").Id;
                var nocheEstrelladaId = _context.Obras.First(o => o.Titulo == "La noche estrellada").Id;
                var girasolesId = _context.Obras.First(o => o.Titulo == "Los girasoles").Id;
                var persistenciaId = _context.Obras.First(o => o.Titulo == "La persistencia de la memoria").Id;
                var nenufaresId = _context.Obras.First(o => o.Titulo == "Nenúfares").Id;
                var besoId = _context.Obras.First(o => o.Titulo == "El beso").Id;

                var obrasExposicion = new[]
                {
                    new ObraEnExposicion { IdObra = guernicaId, IdExposicion = cubismoId },
                    new ObraEnExposicion { IdObra = senoritasId, IdExposicion = cubismoId },
                    new ObraEnExposicion { IdObra = nocheEstrelladaId, IdExposicion = postimpresionismoId },
                    new ObraEnExposicion { IdObra = girasolesId, IdExposicion = postimpresionismoId },
                    new ObraEnExposicion { IdObra = persistenciaId, IdExposicion = surrealismoId },
                    new ObraEnExposicion { IdObra = nenufaresId, IdExposicion = impresionismoId },
                    new ObraEnExposicion { IdObra = besoId, IdExposicion = simbolismoId }
                };

                _context.ObrasEnExposicion.AddRange(obrasExposicion);
                await _context.SaveChangesAsync();
                messages.Add("Relaciones Obra-Exposición creadas exitosamente.");
            }
        }

        private async Task SeedComentariosAsync(List<string> messages)
        {
            if (!_context.Comentarios.Any())
            {
                var personas = _context.Personas.Take(5).ToList();
                var obras = _context.Obras.Take(5).ToList();

                var comentarios = new[]
                {
                    new Comentario {
                        IdPersona = personas[0].Id,
                        IdObra = obras[0].Id,
                        Texto = "Esta obra es realmente impactante, representa perfectamente el horror de la guerra.",
                        FechaComentario = DateTime.Now.AddDays(-10)
                    },
                    new Comentario {
                        IdPersona = personas[1].Id,
                        IdObra = obras[0].Id,
                        Texto = "Los tonos grises transmiten una sensación de desesperanza muy poderosa.",
                        FechaComentario = DateTime.Now.AddDays(-8)
                    },
                    new Comentario {
                        IdPersona = personas[2].Id,
                        IdObra = obras[1].Id,
                        Texto = "Los remolinos en el cielo son hipnóticos, van Gogh era un genio.",
                        FechaComentario = DateTime.Now.AddDays(-6)
                    },
                    new Comentario {
                        IdPersona = personas[3].Id,
                        IdObra = obras[2].Id,
                        Texto = "Siempre me he preguntado qué significan esos relojes derretidos.",
                        FechaComentario = DateTime.Now.AddDays(-4)
                    },
                    new Comentario {
                        IdPersona = personas[4].Id,
                        IdObra = obras[3].Id,
                        Texto = "Los nenúfares de Monet transmiten una paz increíble, me encanta esta serie.",
                        FechaComentario = DateTime.Now.AddDays(-2)
                    },
                    new Comentario {
                        IdPersona = personas[0].Id,
                        IdObra = obras[4].Id,
                        Texto = "El uso del dorado en esta obra es simplemente espectacular.",
                        FechaComentario = DateTime.Now.AddDays(-1)
                    }
                };

                _context.Comentarios.AddRange(comentarios);
                await _context.SaveChangesAsync();
                messages.Add("Comentarios creados exitosamente.");
            }
        }

        private async Task SeedLikesAsync(List<string> messages)
        {
            if (!_context.Likes.Any())
            {
                var personas = _context.Personas.Take(5).ToList();
                var obras = _context.Obras.Take(5).ToList();

                var likes = new[]
                {
                    new Like { IdPersona = personas[0].Id, IdObra = obras[0].Id, FechaLike = DateTime.Now.AddDays(-5) },
                    new Like { IdPersona = personas[1].Id, IdObra = obras[0].Id, FechaLike = DateTime.Now.AddDays(-4) },
                    new Like { IdPersona = personas[2].Id, IdObra = obras[1].Id, FechaLike = DateTime.Now.AddDays(-3) },
                    new Like { IdPersona = personas[3].Id, IdObra = obras[2].Id, FechaLike = DateTime.Now.AddDays(-2) },
                    new Like { IdPersona = personas[4].Id, IdObra = obras[3].Id, FechaLike = DateTime.Now.AddDays(-1) },
                    new Like { IdPersona = personas[0].Id, IdObra = obras[4].Id, FechaLike = DateTime.Now },
                    new Like { IdPersona = personas[1].Id, IdObra = obras[1].Id, FechaLike = DateTime.Now.AddHours(-12) },
                    new Like { IdPersona = personas[2].Id, IdObra = obras[2].Id, FechaLike = DateTime.Now.AddHours(-6) }
                };

                _context.Likes.AddRange(likes);
                await _context.SaveChangesAsync();
                messages.Add("Likes creados exitosamente.");
            }
        }

        private async Task SeedLogActionsAsync(List<string> messages)
        {
            if (!_context.LogActions.Any())
            {
                var logActions = new[]
                {
                    new LogAction {
                        Action = "Login",
                        Details = "Usuario root@admin.com inició sesión",
                        CreatedAt = DateTime.Now.AddDays(-2)
                    },
                    new LogAction {
                        Action = "Creación",
                        Details = "Nueva obra 'Guernica' creada",
                        CreatedAt = DateTime.Now.AddDays(-1).AddHours(-5)
                    },
                    new LogAction {
                        Action = "Actualización",
                        Details = "Precio de obra 'La noche estrellada' actualizado",
                        CreatedAt = DateTime.Now.AddDays(-1).AddHours(-3)
                    },
                    new LogAction {
                        Action = "Eliminación",
                        Details = "Comentario inapropiado eliminado",
                        CreatedAt = DateTime.Now.AddDays(-1).AddHours(-1)
                    },
                    new LogAction {
                        Action = "Registro",
                        Details = "Nuevo usuario registrado: juan@gmail.com",
                        CreatedAt = DateTime.Now
                    }
                };

                _context.LogActions.AddRange(logActions);
                await _context.SaveChangesAsync();
                messages.Add("Logs de acción creados exitosamente.");
            }
        }

        private async Task SeedLogErrorsAsync(List<string> messages)
        {
            if (!_context.LogErrors.Any())
            {
                var logErrors = new[]
                {
                    new LogError {
                        Source = "LoginController",
                        Message = "Intento de inicio de sesión fallido para usuario desconocido",
                        StackTrace = "at Galeria.API.Controllers.LoginController.Authenticate(...)",
                        CreatedAt = DateTime.Now.AddDays(-2)
                    },
                    new LogError {
                        Source = "ObrasController",
                        Message = "Error al cargar imagen para obra nueva",
                        StackTrace = "at Galeria.API.Controllers.ObrasController.UploadImage(...)",
                        CreatedAt = DateTime.Now.AddDays(-1).AddHours(-5)
                    },
                    new LogError {
                        Source = "Database",
                        Message = "Timeout al ejecutar consulta compleja",
                        StackTrace = "at Microsoft.EntityFrameworkCore.Storage...",
                        CreatedAt = DateTime.Now.AddDays(-1).AddHours(-3)
                    },
                    new LogError {
                        Source = "PaymentService",
                        Message = "Error al procesar pago con tarjeta",
                        StackTrace = "at Galeria.Application.Services.PaymentService.ProcessPayment(...)",
                        CreatedAt = DateTime.Now.AddDays(-1).AddHours(-1)
                    },
                    new LogError {
                        Source = "EmailService",
                        Message = "No se pudo enviar correo de confirmación",
                        StackTrace = "at Galeria.Application.Services.EmailService.SendConfirmationEmail(...)",
                        CreatedAt = DateTime.Now
                    }
                };

                _context.LogErrors.AddRange(logErrors);
                await _context.SaveChangesAsync();
                messages.Add("Logs de error creados exitosamente.");
            }
        }
    }
}