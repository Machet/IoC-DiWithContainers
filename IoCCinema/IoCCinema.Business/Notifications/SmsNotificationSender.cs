namespace IoCCinema.Business.Notifications
{
    public class SmsNotificationSender : INotificationSender
    {
        private ISmsSender _sender;
        private ITemplateRepository _templateRepository;
        private IUserRepository _userRepository;

        public SmsNotificationSender(ISmsSender smsSender, ITemplateRepository templateRepository, IUserRepository userRepository)
        {
            _sender = smsSender;
            _templateRepository = templateRepository;
            _userRepository = userRepository;
        }

        public void NotifyReservationReady(int userId, int row, int seatNumber)
        {
            string template = _templateRepository.GetPlainTextTemplate();
            template = string.Format(template, row, seatNumber);
            User user = _userRepository.GetUser(userId);
            _sender.Send(new SmsSettings
            {
                Number = user.MobilePhone,
                Message = template
            });
        }
    }
}