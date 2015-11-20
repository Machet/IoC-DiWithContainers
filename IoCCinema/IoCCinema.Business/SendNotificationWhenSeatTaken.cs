using IoCCinema.Business.DomainEvents;
using IoCCinema.Business.Lotery;
using IoCCinema.Business.Notifications;
using System.Collections.Generic;

namespace IoCCinema.Business
{
    public class SendNotificationWhenSeatTaken :
        IDomainEventHandler<SeatAssignedToUser>,
        IDomainEventHandler<FreeTicketGranted>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly List<INotificationSender> _notifiers;

        public SendNotificationWhenSeatTaken(
            IUserRepository userRepository,
            IRoomRepository roomRepository,
            List<INotificationSender> notifiers)
        {
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _notifiers = notifiers;
        }

        public void Handle(SeatAssignedToUser @event)
        {
            User user = _userRepository.GetUser(@event.UserId);
            Seanse seanse = _roomRepository.GetSeanse(@event.SeanseId);
            foreach (var notifier in _notifiers)
            {
                notifier.NotifyThatReservationIsReady(user, seanse, @event.Seat);
            }
        }

        public void Handle(FreeTicketGranted @event)
        {
            User user = _userRepository.GetUser(@event.UserId);
            foreach (var notifier in _notifiers)
            {
                notifier.NotifyThatFreeTicketGranted(user, @event.CurrentFreeTicketsCount);
            }
        }
    }
}
