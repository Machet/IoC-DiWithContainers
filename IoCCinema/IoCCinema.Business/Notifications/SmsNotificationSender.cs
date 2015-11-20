using System.Collections.Generic;

namespace IoCCinema.Business.Notifications
{
    public class SmsNotificationSender : INotificationSender
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly Dictionary<int, SmsToSend> _currentSmsesToSend;

        public SmsNotificationSender(INotificationRepository notificationRepository, ITemplateRepository templateRepository)
        {
            _notificationRepository = notificationRepository;
            _templateRepository = templateRepository;
            _currentSmsesToSend = new Dictionary<int, SmsToSend>();
        }

        public void NotifyThatReservationIsReady(User user, Seanse seanse, Seat seat)
        {
            string message = _templateRepository.GetReservationPlainTextMessage(seanse, seat);
            if (!CanSendSmsNotification(user))
            {
                return;
            }

            SmsToSend queuedSms = GetSmsFromQueue(user.Id);
            if (queuedSms != null)
            {
                queuedSms.Message = message + " " + queuedSms.Message;
            }
            else
            {
                QueueSms(user, message);
            }
        }

        public void NotifyThatFreeTicketGranted(User user, int freeTicketCount)
        {
            string message = _templateRepository.GetFreeTicketPlainTextMessage(freeTicketCount);
            if (!CanSendSmsNotification(user))
            {
                return;
            }

            SmsToSend queuedSms = GetSmsFromQueue(user.Id);
            if (queuedSms != null)
            {
                queuedSms.Message = queuedSms.Message + " " + message;
            }
            else
            {
                QueueSms(user, message);
            }
        }

        private static bool CanSendSmsNotification(User user)
        {
            return !string.IsNullOrEmpty(user.MobilePhone) && user.ContactBySmslAllowed;
        }

        private SmsToSend GetSmsFromQueue(int userId)
        {
            return _currentSmsesToSend.ContainsKey(userId)
                ? _currentSmsesToSend[userId]
                : null;
        }

        private void QueueSms(User user, string message)
        {
            var smsToSend = new SmsToSend
            {
                UserId = user.Id,
                PhoneNumber = user.MobilePhone,
                Message = message,
                CreationTime = DomainTime.Current.Now
            };

            _currentSmsesToSend[user.Id] = smsToSend;
            _notificationRepository.QueueSms(smsToSend);
        }
    }
}