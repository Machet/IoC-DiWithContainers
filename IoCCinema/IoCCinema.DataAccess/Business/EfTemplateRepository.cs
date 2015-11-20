using IoCCinema.Business;
using IoCCinema.Business.Notifications;

namespace IoCCinema.DataAccess.Business
{
    public class EfTemplateRepository : ITemplateRepository
    {
        public string GetFreeTicketHtmlMessage(int freeTicketCount)
        {
            return "<span style='color: red'/>Congratulations you have won free ticket.</span> You have "
                + freeTicketCount + " free tickets right now.";
        }

        public string GetFreeTicketPlainTextMessage(int freeTicketCount)
        {
            return "Congratulations you have won free ticket. You have "
                + freeTicketCount + " free tickets right now.";
        }

        public string GetReservationHtmlMessage(Seanse seanse, Seat seat)
        {
            return string.Format("You have reserved seat <b>{0}</b> in row <b>{1}</b> for '{2}' at {3}",
                seat.SeatNumber,
                seat.Row,
                seanse.Movie.Title,
                seanse.StartTime);
        }

        public string GetReservationPlainTextMessage(Seanse seanse, Seat seat)
        {
            return string.Format("You have reserved seat {0} in row {1} for '{2}' at {3}",
                seat.SeatNumber,
                seat.Row,
                seanse.Movie.Title,
                seanse.StartTime);
        }
    }
}
