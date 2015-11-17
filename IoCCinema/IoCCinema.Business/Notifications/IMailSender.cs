namespace IoCCinema.Business.Notifications
{
    public interface IMailSender
    {
        void SendMail(MailToSend mail);
    }
}
