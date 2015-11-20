using System;
using System.Collections.Generic;

namespace IoCCinema.Business.Notifications
{
    public class EmailNotificationSender : INotificationSender
    {
        private INotificationRepository _notificationRepository;
        private ITemplateRepository _templateRepository;
        private Dictionary<int, MailToSend> _currentMailsToSend;

        public EmailNotificationSender(INotificationRepository notificationRepository, ITemplateRepository templateRepository)
        {
            _notificationRepository = notificationRepository;
            _templateRepository = templateRepository;
            _currentMailsToSend = new Dictionary<int, MailToSend>();
        }

        public void NotifyThatReservationIsReady(User user, Seanse seanse, Seat seat)
        {
            string message = _templateRepository.GetReservationHtmlMessage(seanse, seat);
            if (!CanSendEmailNotification(user))
            {
                return;
            }

            MailToSend queuedEmail = GetMailFromQueue(user.Id);
            if (queuedEmail != null)
            {
                queuedEmail.Subject = "You have reserved seat and won free ticket";
                queuedEmail.Content = message + Environment.NewLine + queuedEmail.Content;
            }
            else
            {
                QueueEmail(user, "You have reserved seat", message);
            }
        }

        public void NotifyThatFreeTicketGranted(User user, int freeTicketCount)
        {
            string message = _templateRepository.GetFreeTicketHtmlMessage(freeTicketCount);
            if (!CanSendEmailNotification(user))
            {
                return;
            }

            MailToSend queuedEmail = GetMailFromQueue(user.Id);
            if (queuedEmail != null)
            {
                queuedEmail.Subject = "You have reserved seat and won free ticket";
                queuedEmail.Content = queuedEmail.Content + Environment.NewLine + message;
            }
            else
            {
                QueueEmail(user, "You have won free ticket", message);
            }
        }

        private static bool CanSendEmailNotification(User user)
        {
            return !string.IsNullOrEmpty(user.Email) && user.ContactByEmailAllowed;
        }

        private MailToSend GetMailFromQueue(int userId)
        {
            return _currentMailsToSend.ContainsKey(userId)
                ? _currentMailsToSend[userId]
                : null;
        }

        private void QueueEmail(User user, string subject, string message)
        {
            var mailToSend = new MailToSend
            {
                UserId = user.Id,
                EmailTo = user.Email,
                EmailFrom = "noReply@cinema.com",
                Subject = subject,
                Content = message,
                CreationTime = DomainTime.Current.Now
            };

            _currentMailsToSend[mailToSend.UserId] = mailToSend;
            _notificationRepository.QueueMail(mailToSend);
        }
    }
}