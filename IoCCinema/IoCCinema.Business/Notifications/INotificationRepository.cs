using System.Collections.Generic;

namespace IoCCinema.Business.Notifications
{
    public interface INotificationRepository
    {
        void QueueMail(MailToSend mailToSend);
        void QueueSms(SmsToSend smsSettings);
        List<MailToSend> GetUnsentMails();
        List<SmsToSend> GetUnsentSmses();
    }
}
