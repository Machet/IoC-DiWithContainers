namespace IoCCinema.Business.Authentication
{
    public interface ICurrentUserProvider
    {
        int? GetUserId();
        void SetUserId(int id);
    }
}