using System.Linq;
using System.Collections.Generic;
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

        public List<MailToSend> GetUnsentMails()
        {
            return _context.MailsToSend.Where(m => !m.HasBeenSent).ToList();
        }

        public List<SmsToSend> GetUnsentSmses()
        {
            return _context.SmsesToSend.Where(s => !s.HasBeenSent).ToList();
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
