namespace IoCCinema.Business.Commands
{
    public class ReserveSeatCommandHandler : ICommandHandler<ReserveSeatCommand>
    {
        private readonly IRoomRepository _roomRepository;

        public ReserveSeatCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public void Handle(ReserveSeatCommand command)
        {
            Seanse seanse = _roomRepository.GetSeanse(command.SeanseId);
            var seat = new Seat(command.SeatRow, command.SeatNumber);
            seanse.ReserveSeatForUser(command.UserId, seat);
        }
    }
}
