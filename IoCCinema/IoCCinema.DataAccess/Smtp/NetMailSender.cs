using IoCCinema.Business.Notifications;

namespace IoCCinema.DataAccess.Smtp
{
    public class NetMailSender : IMailSender
    {
        private INotificationRepository _notificationRepository;

        public NetMailSender(INotificationRepository notificicationRepository)
        {
            _notificationRepository = notificicationRepository;
        }

        public void Send(MailSettings content)
        {
            _notificationRepository.Add(new Notification
            {
                Text = "Email send."
            });
        }
    }
}
