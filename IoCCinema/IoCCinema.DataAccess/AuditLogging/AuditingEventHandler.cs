using IoCCinema.Business.DomainEvents;

namespace IoCCinema.DataAccess.AuditLogging
{
    public class AuditingEventHandler<T> : IDomainEventHandler<T> where T : IDomainEvent
    {
        private readonly AuditLogger _logger;
        private readonly IDomainEventHandler<T> _innerHandler;

        public AuditingEventHandler(IDomainEventHandler<T> innerHandler, AuditLogger logger)
        {
            _innerHandler = innerHandler;
            _logger = logger;
        }

        public void Handle(T @event)
        {
            _innerHandler.Handle(@event);
            _logger.LogAction("Event " + @event.GetType().Name + " handled by " + _innerHandler.GetType().Name);
        }
    }
}
