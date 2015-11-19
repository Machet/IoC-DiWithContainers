using System.Collections.Generic;

namespace IoCCinema.Presentation
{
    public interface IAuditViewRepository
    {
        List<AuditDTO> GetAuditEntriesForUser(int userId);
    }
}
