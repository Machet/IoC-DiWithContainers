using System;

namespace IoCCinema.DataAccess.AuditLogging
{
    public class AuditLogger
    {
        private readonly ICurrentUserProvider _userProvider;
        private readonly CinemaContext _context;

        public AuditLogger(ICurrentUserProvider userProvider, CinemaContext context)
        {
            _userProvider = userProvider;
            _context = context;
        }

        public void LogAction(string action)
        {
            _context.AuditLogs.Add(new AuditLog
            {
                UserId = _userProvider.GetUserId(),
                ChangeTime = DateTime.Now,
                Changes = action
            });
        }
    }
}
