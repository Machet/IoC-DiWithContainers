namespace IoCCinema.DataAccess.AuditLogging
{
    public interface ICurrentUserProvider
    {
        int GetUserId();
    }
}