using System;
using System.Collections.Generic;
using IoCCinema.Business.AuditLogging;
using IoCCinema.Business.Notifications;

namespace IoCCinema.Business.Commands
{
    public class ReserveSeatCommandHandler : ICommandHandler<ReserveSeatCommand>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly List<INotificationSender> _notifiers;
        private readonly Func<int, AuditLogger> _loggerFactory;

        public ReserveSeatCommandHandler(
            IRoomRepository roomRepository,
            List<INotificationSender> notifiers,
            Func<int, AuditLogger> loggerFactoryFn)
        {
            _roomRepository = roomRepository;
            _notifiers = notifiers;
            _loggerFactory = loggerFactoryFn;
        }

        public void Handle(ReserveSeatCommand command)
        {
            Seanse seanse = _roomRepository.GetSeanse(command.SeanseId);
            var seat = new Seat(command.SeatRow, command.SeatNumber);
            seanse.ReserveSeatForUser(command.UserId, seat);

            foreach (var notifier in _notifiers)
            {
                try
                {
                    notifier.NotifyReservationReady(command.UserId, command.SeatRow, command.SeatNumber);
                }
                catch (Exception)
                {
                    // log 
                }
            }

            _loggerFactory(command.UserId).LogChanges(string.Format("Booked seat {0} in row {1}", command.SeatNumber, command.SeatRow));
        }
    }
}
