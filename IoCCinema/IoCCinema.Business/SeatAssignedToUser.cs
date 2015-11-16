using IoCCinema.Business.DomainEvents;

namespace IoCCinema.Business
{
    public class SeatAssignedToUser : IDomainEvent
    {
        public int SeanseId { get; internal set; }
        public int UserId { get; internal set; }
        public Seat Seat { get; internal set; }
    }
}