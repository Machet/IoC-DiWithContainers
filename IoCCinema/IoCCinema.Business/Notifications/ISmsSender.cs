namespace IoCCinema.Business.Notifications
{
    public interface ISmsSender
    {
        void Send(SmsSettings settings);
    }
}
