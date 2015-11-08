using System.Linq;
using IoCCinema.Business;
using IoCCinema.Business.DTO;

namespace IoCCinema.DataAccess.Repositories
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

        public MovieRoomRelation GetRelation(int movieRoomRelationId)
        {
            return _context.RoomRelations
                .FirstOrDefault(r => r.MovieRoomRelationId == movieRoomRelationId);
        }

        public SeatAssignment GetSeatAssignment(int movieRoomRelationId, int row, int seatNumber)
        {
            return _context.SeatAssignments
                .FirstOrDefault(a => a.MovieRoomRelationId == movieRoomRelationId && a.Row == row && a.SeatNumber == seatNumber);
        }

        public RoomDTO GetRoomByRelation(int movieRoomRelationId)
        {
            return _context.RoomRelations
                .Where(r => r.MovieRoomRelationId == movieRoomRelationId)
                .Select(r => new RoomDTO
                {
                    RowsOfSeats = r.Room.RowsOfSeats,
                    SeatsPerRow = r.Room.SeatsPerRow,
                    TakenSeats = r.SeatAssignments.Select(sa => new SeatDTO
                    {
                        SeatNumber = sa.SeatNumber,
                        SeatRow = sa.Row
                    }).ToList()
                }).FirstOrDefault();
        }
    }
}
