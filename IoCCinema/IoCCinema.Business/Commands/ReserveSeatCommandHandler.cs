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

            if (seanse == null)
            {
                throw new ArgumentException("Seanse doesn't exist");
            }

            if (seanse.StartTime < DomainTime.Current.Now.TimeOfDay)
            {
                throw new InvalidOperationException("Seanse already ended");
            }

            if (seanse.Room.SeatsPerRow < command.SeatRow || command.SeatRow <= 0)
            {
                throw new InvalidOperationException("This room doesn't have such row");
            }

            if (seanse.Room.SeatsPerRow < command.SeatNumber || command.SeatNumber <= 0)
            {
                throw new InvalidOperationException("This room doesn't have such seat");
            }

            if (_roomRepository.GetSeatAssignment(command.SeanseId, command.SeatRow, command.SeatNumber) != null)
            {
                throw new InvalidOperationException("Seat already taken");
            }

            _roomRepository.Add(new SeatAssignment
            {
                SeanseId = command.SeanseId,
                Row = command.SeatRow,
                SeatNumber = command.SeatNumber,
                UserId = command.UserId
            });

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
