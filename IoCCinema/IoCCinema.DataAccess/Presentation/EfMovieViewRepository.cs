using System;
using System.Collections.Generic;
using System.Linq;
using IoCCinema.Presentation;

namespace IoCCinema.DataAccess.Presentation
{
    public class EFHomeViewRepository : IHomeViewRepository
    {
        private readonly CinemaContext _context;

        public EFHomeViewRepository(CinemaContext context)
        {
            _context = context;
        }

        public List<MovieDTO> GetMovies(DateTime start)
        {
            return _context.Movies.Select(m => new MovieDTO
            {
                Title = m.Title,
                Description = m.Description,
                ShowTimes = m.Seanses.Select(r => new SeanseDTO
                {
                    SeanseId = r.SeanseId,
                    StartTime = r.StartTime
                }).ToList()
            }).ToList();
        }

        public RoomDTO GetRoomBySeanse(int seanseId)
        {
            return _context.Seanses
                .Where(r => r.SeanseId == seanseId)
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
