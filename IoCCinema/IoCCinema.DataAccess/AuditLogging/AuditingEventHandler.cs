using IoCCinema.Business.DomainEvents;
using Newtonsoft.Json;
using System;

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
            string serializedEvent = JsonConvert.SerializeObject(@event);
            _logger.LogAction("Event raised " + typeof(T).Name + Environment.NewLine + serializedEvent);
            _innerHandler.Handle(@event);
        }
    }
}
