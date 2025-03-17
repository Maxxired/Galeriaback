using Galeria.Domain.Entities.Logs;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Log
{
    public interface ILogErrorRepository : IBaseRepository<LogError>
    {
        Task LogErrorAsync(Exception ex, string source);
    }
}
