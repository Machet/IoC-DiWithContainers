using IoCCinema.Business;
using IoCCinema.Business.Authentication;
using System;
using System.Linq;

namespace IoCCinema.DataAccess.Business
{
    public class EfAuthenticationRepository : IAuthenticationRepository
    {
        private readonly CinemaContext _context;

        public EfAuthenticationRepository(CinemaContext context)
        {
            _context = context;
        }

        public void Add(LoginAttempt loginAttempt)
        {
            _context.LoginAttempts.Add(loginAttempt);
        }

        public User FindUserByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.Name == userName);
        }

        public LoginAttempt GetLoginAttemptById(Guid id)
        {
            return _context.LoginAttempts.Find(id);
        }
    }
}
