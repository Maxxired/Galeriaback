using Microsoft.AspNetCore.Identity;
using Galeria.Domain.Entities;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Infraestructure;

using static Galeria.Domain.Common.Util.Enums;


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
                    new ApplicationUser { UserName = "ocelot@gmail.com", Email = "ocelot@gmail.com", IsDeleted = false }
                };

                foreach (var user in users)
                {
                    var result = await _userManager.CreateAsync(user, "Niux123?");
                    if (result.Succeeded)
                    {
                        messages.Add($"Usuario {user.UserName} creado exitosamente.");

                        if (user.UserName == "root@admin.com")
                            await _userManager.AddToRoleAsync(user, "Admin");
                        else if (user.UserName == "socio@test.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "cliente@test.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "juan@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "alonso@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "andres@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "diego@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "merida@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "jaime@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "alison@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "cochi@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "david@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "erika@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Usuario");
                        else if (user.UserName == "ocelot@gmail.com")
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
                    new Persona { Nombres = "John", Apellidos = "Doe", Edad = 30, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "root@admin.com").Id },
                    new Persona { Nombres = "Sarah", Apellidos = "Lee", Edad = 28, Sexo = Sexo.FEMENINO, IdApplicationUser = _context.Users.First(u => u.UserName == "socio@test.com").Id },
                    new Persona { Nombres = "Mark", Apellidos = "Parker", Edad = 25, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "cliente@test.com").Id },
                    new Persona { Nombres = "Juan", Apellidos = "Perez", Edad = 35, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "juan@gmail.com").Id },
                    new Persona { Nombres = "Alonso", Apellidos = "Garcia", Edad = 40, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "alonso@gmail.com").Id },
                    new Persona { Nombres = "Andres", Apellidos = "Hernandez", Edad = 45, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "andres@gmail.com").Id },
                    new Persona { Nombres = "Diego", Apellidos = "Jimenez", Edad = 50, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "diego@gmail.com").Id },
                    new Persona { Nombres = "Merida", Apellidos = "Lopez", Edad = 55, Sexo = Sexo.FEMENINO, IdApplicationUser = _context.Users.First(u => u.UserName == "merida@gmail.com").Id },
                    new Persona { Nombres = "Jaime", Apellidos = "Gutierrez", Edad = 60, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "jaime@gmail.com").Id },
                    new Persona { Nombres = "Alison", Apellidos = "Gomez", Edad = 65, Sexo = Sexo.FEMENINO, IdApplicationUser = _context.Users.First(u => u.UserName == "alison@gmail.com").Id },
                    new Persona { Nombres = "Cochi", Apellidos = "Gutierrez", Edad = 70, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "cochi@gmail.com").Id },
                    new Persona { Nombres = "David", Apellidos = "Gomez", Edad = 75, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "david@gmail.com").Id },
                    new Persona { Nombres = "Erika", Apellidos = "Gomez", Edad = 80, Sexo = Sexo.FEMENINO, IdApplicationUser = _context.Users.First(u => u.UserName == "erika@gmail.com").Id },
                    new Persona { Nombres = "Ocelot", Apellidos = "Gomez", Edad = 85, Sexo = Sexo.MASCULINO, IdApplicationUser = _context.Users.First(u => u.UserName == "ocelot@gmail.com").Id }

                };

                _context.Personas.AddRange(personas);
                _context.SaveChanges();
                messages.Add("Personas creadas exitosamente.");
            }

            return messages;
        }


        private async Task CreateRolesAsync(List<string> messages)
        {
            string[] roleNames = { "Admin", "Usuario" };
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
    }
}
