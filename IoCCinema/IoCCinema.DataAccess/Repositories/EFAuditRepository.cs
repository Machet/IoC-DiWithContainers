using IoCCinema.Business.AuditLogging;

namespace IoCCinema.DataAccess.Repositories
{
    public class EFAuditRepository : IAuditRepository
    {
        private readonly CinemaContext _context;

        public EFAuditRepository(CinemaContext context)
        {
            _context = context;
        }

        public void Add(AuditLog auditLog)
        {
            _context.AuditLogs.Add(auditLog);
            _context.SaveChanges();
        }
    }
}
