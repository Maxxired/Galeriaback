using Galeria.Domain.Entities.Logs;
using Galeria.Infraestructure.Interfaces.Log;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Repositories.Log
{
    public class LogActionRepository : BaseRepository<LogAction>, ILogActionRepository
    {
        private readonly ApplicationDbContext _context;
        public LogActionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task LogActionAsync(string action, string details = null)
        {
            var log = new LogAction
            {
                Action = action,
                Details = details
            };

            _context.LogActions.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
