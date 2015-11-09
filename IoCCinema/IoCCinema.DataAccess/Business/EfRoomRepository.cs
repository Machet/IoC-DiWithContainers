using System.Linq;
using IoCCinema.Business;

namespace IoCCinema.DataAccess.Business
{
    public class EfRoomRepository : IRoomRepository
    {
        private CinemaContext _context;

        public EfRoomRepository(CinemaContext context)
        {
            _context = context;
        }

        public void Add(SeatAssignment seatAssignment)
        {
            _context.SeatAssignments.Add(seatAssignment);
            _context.SaveChanges();
        }

        public Seanse GetSeanse(int seanseId)
        {
            return _context.Seanses
                .FirstOrDefault(r => r.SeanseId == seanseId);
        }

        public SeatAssignment GetSeatAssignment(int seanseId, int row, int seatNumber)
        {
            return _context.SeatAssignments
                .FirstOrDefault(a => a.SeanseId == seanseId && a.Row == row && a.SeatNumber == seatNumber);
        }
    }
}
