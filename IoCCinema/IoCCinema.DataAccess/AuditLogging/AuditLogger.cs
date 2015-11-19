using IoCCinema.Business.Authentication;
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
            if (!_userProvider.GetUserId().HasValue)
            {
                return;
            }

            _context.AuditLogs.Add(new AuditLog
            {
                UserId = _userProvider.GetUserId().Value,
                ChangeTime = DateTime.Now,
                Changes = action
            });
        }
    }
}
