namespace IoCCinema.Common
{
    public interface IEventHandler<T> where T : IDomainEvent
    {
        void Handle(T @event);
    }
}
