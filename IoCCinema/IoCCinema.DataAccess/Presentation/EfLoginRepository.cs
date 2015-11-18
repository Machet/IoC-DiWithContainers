using IoCCinema.Presentation;
using System;
using System.Linq;

namespace IoCCinema.DataAccess.Presentation
{
    public class EfLoginRepository : ILoginRepository
    {
        private readonly CinemaContext _context;

        public EfLoginRepository(CinemaContext context)
        {
            _context = context;
        }

        public LoginAttemptDTO GetLoginAttemptById(Guid id)
        {
            return _context.LoginAttempts
                .Select(la => new LoginAttemptDTO
                {
                    LoginAttemptId = la.LoginAttemptId,
                    Message = la.Message,
                    Succeeded = la.Succeeded,
                    UserId = la.UserId,
                    UserName = la.UserId.HasValue
                        ? _context.Users.Where(u => u.Id == la.UserId).Select(u => u.Name).FirstOrDefault()
                        : null,
                }).FirstOrDefault(la => la.LoginAttemptId == id);
        }
    }
}
