namespace IoCCinema.Business.Notifications
{
    public interface ITemplateRepository
    {
        string GetReservationPlainTextMessage(Seanse seanse, Seat seat);
        string GetReservationHtmlMessage(Seanse seanse, Seat seat);
    }
}
