namespace IoCCinema.Business.Notifications
{
    public interface INotificationSender
    {
        void NotifyReservationReady(User user, Seanse seanse, Seat seat);
    }
}