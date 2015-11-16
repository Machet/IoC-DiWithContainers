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

        public void QueueMail(MailToSend mailToSend)
        {
            _context.MailsToSend.Add(mailToSend);
        }

        public void QueueSms(SmsToSend smsToSend)
        {
            _context.SmsesToSend.Add(smsToSend);
        }
    }
}
