namespace IoCCinema.Business.Notifications
{
    public interface INotificationSender
    {
        void NotifyReservationReady(int userId, int row, int seatNumber);
    }
}