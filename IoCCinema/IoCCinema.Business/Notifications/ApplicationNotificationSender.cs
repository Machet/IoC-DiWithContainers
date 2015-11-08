namespace IoCCinema.Business.Notifications
{
    public class ApplicationNotificationSender : INotificationSender
    {
        private ITemplateRepository _templateRepository;
        private INotificationRepository _notificationRepository;

        public ApplicationNotificationSender(ITemplateRepository templateRepository, INotificationRepository notificationRepository)
        {
            _templateRepository = templateRepository;
            _notificationRepository = notificationRepository;
        }

        public void NotifyReservationReady(int userId, int row, int seatNumber)
        {
            string template = _templateRepository.GetHtmlTemplate();
            template = string.Format(template, row, seatNumber);
            _notificationRepository.Add(new Notification
            {
                Text = template
            });
        }
    }
}