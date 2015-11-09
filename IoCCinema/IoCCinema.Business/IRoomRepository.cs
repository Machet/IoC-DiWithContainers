namespace IoCCinema.Business
{
    public interface IRoomRepository
    {
        void Add(SeatAssignment seatAssignment);
        Seanse GetSeanse(int seanseId);
        SeatAssignment GetSeatAssignment(int seanseId, int row, int seatNumber);
    }
}
