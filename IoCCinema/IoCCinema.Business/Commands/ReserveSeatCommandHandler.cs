using IoCCinema.Business.Lotery;

namespace IoCCinema.Business.Commands
{
    public class ReserveSeatCommandHandler : ICommandHandler<ReserveSeatCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IWinChanceCalculatorFactory _calculatorFactory;

        public ReserveSeatCommandHandler(IUserRepository userRepository, IRoomRepository roomRepository, IWinChanceCalculatorFactory calculatorFactory)
        {
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _calculatorFactory = calculatorFactory;
        }

        public void Handle(ReserveSeatCommand command)
        {
            Seanse seanse = _roomRepository.GetSeanse(command.SeanseId);
            User user = _userRepository.GetUser(command.UserId);

            var seat = new Seat(command.SeatRow, command.SeatNumber);
            seanse.ReserveSeatForUser(command.UserId, seat);

            FreeTicketLotery lottery = new FreeTicketLotery(_calculatorFactory);
            lottery.DrawFreeTicketForUser(user);
        }
    }
}
