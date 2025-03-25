using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Galeria.Application.Interfaces.Obras;
using Galeria.Application.Services.Base;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Interfaces.Log;
using Galeria.Infraestructure.Interfaces.Obras;
using Galeria.Infraestructure.Interfaces.Usuarios.Artistas;
using Galeria.Infraestructure.Repositories.Obras;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Galeria.Application.Services.Obras
{
    public class ObraService : ServiceBase<Obra, ObraDTO>,IObraService
    {
        private readonly IMapper _mapper;
        private readonly IObraRepository _repository;
        private readonly IObraCategoriaRepository _obraCategoriaRepository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public ObraService(IObraRepository repository, IMapper mapper, ILogActionRepository logAction, ILogErrorRepository logError, IWebHostEnvironment env, IObraCategoriaRepository obraCategoriaRepository) : base(mapper, repository, logAction, logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
            _env = env;
            _obraCategoriaRepository = obraCategoriaRepository;
        }

        public async Task<ResponseHelper> SubirImagenObra(int idObra, IFormFile archivo)
        {
            var _uploadBasePath = GetUploadBasePath();

            if (archivo == null || archivo.Length == 0)
            {
                throw new ArgumentException("El archivo no puede ser nulo o estar vacío.", nameof(archivo));
            }

            var extension = Path.GetExtension(archivo.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("El tipo de archivo no es permitido. Solo se aceptan .jpg, .jpeg, .png y .gif.");
            }

            ResponseHelper response = new();

            try
            {
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_uploadBasePath, fileName);

                CrearCarpetaSiNoExiste(_uploadBasePath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                var rutaArchivo = Path.Combine("Uploads", "FotosObras", fileName);

                var query = await _repository.SubirImagenObra(idObra, rutaArchivo);

                response.Success = query > 0;
                response.Message = query > 0 ? "La imagen fue subida con éxito." : "No se pudo subir la imagen.";

                string dataAsJson = JsonSerializer.Serialize(new { idObra, rutaArchivo });
                Log.Information(response.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> ActualizarImagenObra(int idObra, IFormFile archivo)
        {
            var _uploadBasePath = GetUploadBasePath();

            if (archivo == null || archivo.Length == 0)
            {
                throw new ArgumentException("El archivo no puede ser nulo o estar vacío.", nameof(archivo));
            }

            var extension = Path.GetExtension(archivo.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("El tipo de archivo no es permitido. Solo se aceptan .jpg, .jpeg, .png y .gif.");
            }

            ResponseHelper response = new();

            try
            {
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_uploadBasePath, fileName);

                CrearCarpetaSiNoExiste(_uploadBasePath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                var rutaArchivo = Path.Combine("Uploads", "FotosObras", fileName);

                var query = await _repository.ActualizarImagenObra(idObra, rutaArchivo);

                response.Success = query > 0;
                response.Message = query > 0 ? "La imagen fue actualizada con éxito." : "No se pudo actualizar la imagen.";

                string dataAsJson = JsonSerializer.Serialize(new { idObra, rutaArchivo });
                Log.Information(response.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> EliminarImagenObra(int idObra)
        {
            ResponseHelper response = new();

            try
            {
                var query = await _repository.EliminarImagenObra(idObra);

                response.Success = query > 0;
                response.Message = query > 0 ? "La imagen fue eliminada con éxito." : "No se pudo eliminar la imagen.";

                Log.Information(response.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                response.Message = ex.Message;
            }

            return response;
        }

        private string GetUploadBasePath()
        {
            return Path.Combine(_env.WebRootPath, "Uploads", "FotosObras");
        }

        private void CrearCarpetaSiNoExiste(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public async Task<int> CreateObraAsync(ObraCreateDTO obraDto)
        {
            var obra = new Obra
            {
                Titulo = obraDto.Titulo,
                Descripcion = obraDto.Descripcion,
                Precio = obraDto.Precio,
                ImagenUrl = obraDto.ImagenUrl,
                IdArtista = obraDto.ArtistaId,
                Slug = await GenerateUniqueSlug(obraDto.Titulo)
            };

            if (obraDto.CategoriaIds != null && obraDto.CategoriaIds.Any())
            {
                obra.ObrasCategorias = obraDto.CategoriaIds.Select(ci => new ObraCategoria { IdCategoria = ci }).ToList();
            }

            var createdObra = await _repository.InsertAsync(obra);

            return createdObra;
        }

        public async Task<int> UpdateObraAsync(int id, ObraUpdateDTO obraDto)
        {
            var obra = await _repository.GetByIdAsync(id);
            if (obra == null) return 0;

            if (obra.Titulo != obraDto.Titulo)
            {
                obra.Slug = await GenerateUniqueSlug(obraDto.Titulo);
            }

            obra.Titulo = obraDto.Titulo;
            obra.Descripcion = obraDto.Descripcion;
            obra.Precio = obraDto.Precio;
            obra.ImagenUrl = obraDto.ImagenUrl;

            if (obraDto.CategoriaIds != null)
            {
                await _obraCategoriaRepository.RemoveByObraIdAsync(id);
                var nuevasCategorias = obraDto.CategoriaIds.Select(ci => new ObraCategoria { IdObra = id, IdCategoria = ci }).ToList();
                await _obraCategoriaRepository.InsertManyAsync(nuevasCategorias);
            }

            return await _repository.UpdateAsync(obra);
        }


        public async Task<IEnumerable<ObraQueryDTO>> GetByTituloAsync(string titulo, int skip, int take)
        {
            try
            {
                return await _repository.GetByTituloAsync(titulo, skip, take);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                throw new ApplicationException("Error al obtener obras por etiqueta.", ex);
            }
        }

        public async Task<IEnumerable<ObraQueryDTO>> GetByEtiquetaNombreAsync(string etiqueta, int skip, int take)
        {
            try
            {
                return await _repository.GetByEtiquetaNombreAsync(etiqueta, skip, take);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                throw new ApplicationException("Error al obtener obras por etiqueta.", ex);
            }
        }

        public async Task<IEnumerable<ObraQueryDTO>> GetByAutorNombreAsync(string autor, int skip, int take)
        {
            try
            {
                return await _repository.GetByAutorNombreAsync(autor, skip, take);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                throw new ApplicationException("Error al obtener obras por autor.", ex);
            }
        }

        public async Task<IEnumerable<ObraQueryDTO>> GetLibrosMasGustadosAsync()
        {
            try
            {
                return await _repository.GetLibrosMasGustadosAsync();
            }
            catch (Exception ex)
            {
                await LogError(ex);
                throw new ApplicationException("Error al obtener los libros más gustados.", ex);
            }
        }

        public async Task<ObrasDTO> GetBySlugAsync(string slug)
        {
            try
            {
                return await _repository.GetBySlugAsync(slug);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                throw new ApplicationException("Error al obtener la obra por slug.", ex);
            }
        }

        public async Task<IEnumerable<ObrasDTO>> GetLibrosByAutorAsync(string token)
        {
            try
            {
                return await _repository.GetLibrosByAutorAsync(token);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                throw new ApplicationException("Error al obtener los libros por autor.", ex);
            }
        }

        private async Task<string> GenerateUniqueSlug(string titulo)
        {
            string baseSlug = NormalizeString(titulo);
            string slug = baseSlug;
            int count = 1;

            while (await _repository.IsSlugTakenAsync(slug))
            {
                slug = $"{baseSlug}_{count}";
                count++;
            }

            return slug;
        }

        private string NormalizeString(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            string normalized = input.Normalize(System.Text.NormalizationForm.FormD);
            normalized = new string(normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
            normalized = normalized.Normalize(System.Text.NormalizationForm.FormC);

            normalized = normalized.Replace(" ", "").ToLower();
            normalized = normalized.Replace("+", "").ToLower();

            normalized = normalized.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
                                   .Replace("ñ", "n").Replace("Á", "a").Replace("É", "e").Replace("Í", "i").Replace("Ó", "o")
                                   .Replace("Ú", "u").Replace("Ñ", "n");

            return normalized;
        }
        public class ObraCreateDTO
        {
            public bool isDeleted { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public string ImagenUrl { get; set; }
            public int ArtistaId { get; set; }
            public List<int> CategoriaIds { get; set; }
        }

        public class ObraUpdateDTO
        {
            public bool isDeleted { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public string ImagenUrl { get; set; }
            public List<int> CategoriaIds { get; set; }
        }
    }
}
