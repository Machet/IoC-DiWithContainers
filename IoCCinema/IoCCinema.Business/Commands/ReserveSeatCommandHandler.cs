using System;
using IoCCinema.Business.AuditLogging;

namespace IoCCinema.Business.Commands
{
    public class ReserveSeatCommandHandler : ICommandHandler<ReserveSeatCommand>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly Func<int, AuditLogger> _loggerFactory;

        public ReserveSeatCommandHandler(
            IRoomRepository roomRepository,
            Func<int, AuditLogger> loggerFactoryFn)
        {
            _roomRepository = roomRepository;
            _loggerFactory = loggerFactoryFn;
        }

        public void Handle(ReserveSeatCommand command)
        {
            Seanse seanse = _roomRepository.GetSeanse(command.SeanseId);
            var seat = new Seat(command.SeatRow, command.SeatNumber);
            seanse.ReserveSeatForUser(command.UserId, seat);

            _loggerFactory(command.UserId).LogChanges(string.Format("Booked seat {0} in row {1}", command.SeatNumber, command.SeatRow));
        }
    }
}
