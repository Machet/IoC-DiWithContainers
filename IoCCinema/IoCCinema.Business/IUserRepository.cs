namespace IoCCinema.Business
{
    public interface IUserRepository
    {
        User GetUser(int userId);
        int GetReservationsCountForUser(int userId);
    }
}
