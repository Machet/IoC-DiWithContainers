using IoCCinema.Business.Notifications;

namespace IoCCinema.DataAccess.Business
{
    public class EfNotificationRepository : INotificationRepository
    {
        private readonly CinemaContext _context;

        public EfNotificationRepository(CinemaContext context)
        {
            _context = context;
        }

        public void Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
    }
}
