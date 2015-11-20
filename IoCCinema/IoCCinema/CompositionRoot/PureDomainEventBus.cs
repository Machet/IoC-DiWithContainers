using IoCCinema.Business;
using System.Collections.Generic;
using IoCCinema.Business.DomainEvents;
using IoCCinema.Business.Notifications;
using IoCCinema.DataAccess.AuditLogging;
using IoCCinema.Business.Lotery;

namespace IoCCinema.CompositionRoot
{
    public class PureDomainEventBus : DomainEventBus
    {
        protected override IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>()
        {
            if (typeof(T) == typeof(SeatAssignedToUser))
            {
                var perRequestStore = PerRequestStore.Current;
                var handler = (IDomainEventHandler<T>)perRequestStore.SendNotificationHandler.Value;

                yield return new AuditingEventHandler<T>(handler, perRequestStore.AuditLogger.Value);
            }

            if (typeof(T) == typeof(FreeTicketGranted))
            {
                var perRequestStore = PerRequestStore.Current;
                var handler = (IDomainEventHandler<T>)perRequestStore.SendNotificationHandler.Value;

                yield return new AuditingEventHandler<T>(handler, perRequestStore.AuditLogger.Value);
            }

            yield break;
        }
    }
}