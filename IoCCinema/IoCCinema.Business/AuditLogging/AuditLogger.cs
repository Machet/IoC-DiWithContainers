using System;

namespace IoCCinema.Business.AuditLogging
{
    public class AuditLogger
    {
        private IAuditRepository _repository;
        private int _userId;

        public AuditLogger(int userId, IAuditRepository auditRepository)
        {
            _repository = auditRepository;
            _userId = userId;
        }

        public void LogChanges(string action)
        {
            _repository.Add(new AuditLog
            {
                UserId = _userId,
                ChangeTime = DateTime.Now,
                Changes = action
            });
        }
    }
}
