namespace IoCCinema.Business
{
    public class Seat
    {
        public int Row { get; private set; }
        public int SeatNumber { get; private set; }

        public Seat(int seatRow, int seatNumber)
        {
            Row = seatRow;
            SeatNumber = seatNumber;
        }
    }
}
