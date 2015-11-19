using IoCCinema.Presentation;
using System.Linq;

namespace IoCCinema.DataAccess.Presentation
{
    public class EfNotificationViewRepository : INotificationViewRepository
    {
        private readonly CinemaContext _context;

        public EfNotificationViewRepository(CinemaContext context)
        {
            _context = context;
        }

        public NotificationsDTO GetNotificationsForUser(int userId)
        {
            var notifications = new NotificationsDTO();
            notifications.Smses = _context.SmsesToSend
                .Where(s => s.UserId == userId)
                .Select(s => new NotificationsDTO.Sms
                {
                    CreationTime = s.CreationTime,
                    Message = s.Message,
                    HasBeenSent = s.HasBeenSent
                })
                .OrderByDescending(a => a.CreationTime)
                .ToList();

            notifications.Emails = _context.MailsToSend
                .Where(e => e.UserId == userId)
                .Select(e => new NotificationsDTO.Email
                {
                    CreationTime = e.CreationTime,
                    Subject = e.Subject,
                    Content = e.Content,
                    From = e.EmailFrom,
                    HasBeenSent = e.HasBeenSent
                })
                .OrderByDescending(a => a.CreationTime)
                .ToList();

            return notifications;
        }
    }
}
