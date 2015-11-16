using System;

namespace IoCCinema.Business.Notifications
{
    public class EmailNotificationSender : INotificationSender
    {
        private INotificationRepository _notificationRepository;
        private ITemplateRepository _templateRepository;

        public EmailNotificationSender(INotificationRepository notificationRepository, ITemplateRepository templateRepository)
        {
            _notificationRepository = notificationRepository;
            _templateRepository = templateRepository;
        }

        public void NotifyReservationReady(User user, Seanse seanse, Seat seat)
        {
            string template = _templateRepository.GetReservationHtmlMessage(seanse, seat);
            if (!string.IsNullOrEmpty(user.Email) && user.ContactByEmailAllowed)
            {
                _notificationRepository.QueueMail(new MailToSend
                {
                    EmailTo = user.Email,
                    EmailFrom = "noReply@cinema.com",
                    Subject = "Result",
                    Content = template
                });
            }
        }
    }
}