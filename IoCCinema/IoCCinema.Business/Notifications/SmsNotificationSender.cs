namespace IoCCinema.Business.Notifications
{
    public class SmsNotificationSender : INotificationSender
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ITemplateRepository _templateRepository;

        public SmsNotificationSender(INotificationRepository notificationRepository, ITemplateRepository templateRepository)
        {
            _notificationRepository = notificationRepository;
            _templateRepository = templateRepository;
        }

        public void NotifyReservationReady(User user, Seanse seanse, Seat seat)
        {
            string message = _templateRepository.GetReservationPlainTextMessage(seanse, seat);
            if (!string.IsNullOrEmpty(user.MobilePhone) && user.ContactBySmslAllowed)
            {
                _notificationRepository.QueueSms(new SmsToSend
                {
                    Number = user.MobilePhone,
                    Message = message
                });
            }
        }
    }
}