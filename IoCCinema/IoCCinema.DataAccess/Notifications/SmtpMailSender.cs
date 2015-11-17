using IoCCinema.Business.Notifications;

namespace IoCCinema.DataAccess.Notifications
{
    public class SmtpMailSender : IMailSender
    {
        public void SendMail(MailToSend mail)
        {
            // create smtp client
            // prepare mail
            // send it
        }
    }
}
