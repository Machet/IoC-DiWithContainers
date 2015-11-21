using IoCCinema.Business.DomainEvents;
using Newtonsoft.Json;
using System;

namespace IoCCinema.DataAccess.AuditLogging
{
    public class AuditOccurrenceEventHandler<T> : IDomainEventHandler<T> where T : IDomainEvent
    {
        private readonly AuditLogger _logger;

        public AuditOccurrenceEventHandler(AuditLogger logger)
        {
            _logger = logger;
        }

        public void Handle(T @event)
        {
            string serializedEvent = JsonConvert.SerializeObject(@event);
            _logger.LogAction("Event raised " + typeof(T).Name + Environment.NewLine + serializedEvent);
        }
    }
}
