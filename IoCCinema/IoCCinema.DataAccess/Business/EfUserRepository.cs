using IoCCinema.Business;

namespace IoCCinema.DataAccess.Business
{
    public class EfUserRepository : IUserRepository
    {
        private readonly CinemaContext _context;

        public EfUserRepository(CinemaContext context)
        {
            _context = context;
        }

        public User GetUser(int userId)
        {
            return _context.Users.Find(userId);
        }
    }
}
