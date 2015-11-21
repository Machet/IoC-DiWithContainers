using IoCCinema.Business;
using IoCCinema.Business.DomainEvents;
using IoCCinema.Business.Lotery;
using IoCCinema.DataAccess.AuditLogging;
using System.Collections.Generic;

namespace IoCCinema.CompositionRoot
{
    public class PureDomainEventBus : DomainEventBus
    {
        protected override IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>()
        {
            var perRequestStore = PerRequestStore.Current;
            yield return new AuditOccurrenceEventHandler<T>(perRequestStore.AuditLogger.Value);

            if (typeof(T) == typeof(SeatAssignedToUser))
            {
                var handler = (IDomainEventHandler<T>)perRequestStore.SendNotificationHandler.Value;
                yield return new AuditingEventHandler<T>(handler, perRequestStore.AuditLogger.Value);
            }

            if (typeof(T) == typeof(FreeTicketGranted))
            {
                var handler = (IDomainEventHandler<T>)perRequestStore.SendNotificationHandler.Value;
                yield return new AuditingEventHandler<T>(handler, perRequestStore.AuditLogger.Value);
            }

            yield break;
        }
    }
}