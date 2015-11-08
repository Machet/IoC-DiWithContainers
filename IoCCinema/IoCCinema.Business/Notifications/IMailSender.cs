namespace IoCCinema.Business.Notifications
{
    public interface IMailSender
    {
        void Send(MailSettings content);
    }
}
