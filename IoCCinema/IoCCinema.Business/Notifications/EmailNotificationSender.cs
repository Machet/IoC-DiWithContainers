using IoCCinema.Business;
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
                    UserId = user.Id,
                    EmailTo = user.Email,
                    EmailFrom = "noReply@cinema.com",
                    Subject = "You have reserved seat",
                    Content = template,
                    CreationTime = DomainTime.Current.Now
                });
            }
        }
    }
}