using IoCCinema.DataAccess.AuditLogging;

namespace IoCCinema.CompositionRoot
{
    public class ContextUserProvider : ICurrentUserProvider
    {
        public int GetUserId()
        {
            return 1;
        }
    }
}