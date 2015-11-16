namespace IoCCinema.Business.Notifications
{
    public interface INotificationRepository
    {
        void QueueMail(MailToSend mailToSend);
        void QueueSms(SmsToSend smsSettings);
    }
}
