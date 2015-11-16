namespace IoCCinema.Business.DomainEvents
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void Handle(T @event);
    }
}