namespace IoCCinema.Business.Notifications
{
    public class EmailNotificationSender : INotificationSender
    {
        private IMailSender _sender;
        private ITemplateRepository _templateRepository;
        private IUserRepository _userRepository;

        public EmailNotificationSender(IMailSender mailSender, ITemplateRepository templateRepository, IUserRepository userRepository)
        {
            _sender = mailSender;
            _templateRepository = templateRepository;
            _userRepository = userRepository;
        }

        public void NotifyReservationReady(int userId, int row, int seatNumber)
        {
            string template = _templateRepository.GetHtmlTemplate();
            template = string.Format(template, row, seatNumber);
            User user = _userRepository.GetUser(userId);
            _sender.Send(new MailSettings
            {
                EmailTo = user.Email,
                EmailFrom = "noReply@cinema.com",
                Subject = "Result",
                Content = template
            });
        }
    }
}