namespace IoCCinema.Business
{
    public class SeatAssignment
    {
        public int SeatAssignmentId { get; set; }
        public int MovieRoomRelationId { get; set; }
        public int UserId { get; set; }
        public int Row { get; set; }
        public int SeatNumber { get; set; }
    }
}
