using IoCCinema.Business.DomainEvents;

namespace IoCCinema.Business.Lotery
{
    public class FreeTicketGranted : IDomainEvent
    {
        public int UserId { get; private set; }
        public int CurrentFreeTicketsCount { get; private set; }

        public FreeTicketGranted(int userId, int currentFreeTicketsCount)
        {
            UserId = userId;
            CurrentFreeTicketsCount = currentFreeTicketsCount;
        }
    }
}
