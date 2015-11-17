namespace IoCCinema.Business.Notifications
{
    public interface ISmsSender
    {
        void SendSms(SmsToSend sms);
    }
}
