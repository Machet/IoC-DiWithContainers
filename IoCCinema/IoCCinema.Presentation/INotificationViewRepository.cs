namespace IoCCinema.Presentation
{
    public interface INotificationViewRepository
    {
        NotificationsDTO GetNotificationsForUser(int userId);
    }
}
