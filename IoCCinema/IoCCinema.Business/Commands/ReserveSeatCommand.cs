namespace IoCCinema.Business.Commands
{
    public class ReserveSeatCommand : ICommand
    {
        public int SeanseId { get; set; }
        public int SeatNumber { get; set; }
        public int SeatRow { get; set; }
        public int UserId { get; set; }
    }
}
