using IoCCinema.Business;
using System.Collections.Generic;
using IoCCinema.Business.DomainEvents;
using IoCCinema.Business.Notifications;

namespace IoCCinema.CompositionRoot
{
    public class PureDomainEventBus : DomainEventBus
    {
        protected override IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>()
        {
            if (typeof(T) == typeof(SeatAssignedToUser))
            {
                var perRequestStore = PerRequestStore.Current;
                yield return (IDomainEventHandler<T>)new SendNotificationWhenSeatTaken(
                    perRequestStore.UserRepository.Value,
                    perRequestStore.RoomRepository.Value, new List<INotificationSender>
                    {
                        new SmsNotificationSender(perRequestStore.NotificationRepository.Value, perRequestStore.TemplateRepository.Value),
                        new EmailNotificationSender(perRequestStore.NotificationRepository.Value, perRequestStore.TemplateRepository.Value)
                    });
            }

            yield break;
        }
    }
}