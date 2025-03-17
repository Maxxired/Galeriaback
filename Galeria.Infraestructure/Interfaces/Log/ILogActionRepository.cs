using Galeria.Domain.Entities.Logs;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Log
{
    public interface ILogActionRepository : IBaseRepository<LogAction>
    {
        Task LogActionAsync(string action, string details = null);
    }
}
