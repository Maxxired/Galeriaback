using Galeria.Domain.Entities.Logs;
using Galeria.Infraestructure.Interfaces.Log;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Repositories.Log
{
    public class LogErrorRepository : BaseRepository<LogError>, ILogErrorRepository
    {
        private readonly ApplicationDbContext _context;
        public LogErrorRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task LogErrorAsync(Exception ex, string source)
        {
            var log = new LogError
            {
                Source = source,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            };

            _context.LogErrors.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
