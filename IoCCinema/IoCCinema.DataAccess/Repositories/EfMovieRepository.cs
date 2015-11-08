using System;
using System.Collections.Generic;
using System.Linq;
using IoCCinema.Business.DTO;

namespace IoCCinema.DataAccess.Repositories
{
    public class EfMovieRepository : IMovieRepository
    {
        private readonly CinemaContext _context;

        public EfMovieRepository(CinemaContext context)
        {
            _context = context;
        }

        public List<MovieDTO> GetMovies(DateTime start)
        {
            return _context.Movies.Select(m => new MovieDTO
            {
                Title = m.Title,
                Description = m.Description,
                ShowTimes = m.RoomRelations.Select(r => new SeanseDTO
                {
                    MovieRoomRelationId = r.MovieRoomRelationId,
                    StartTime = r.StartTime
                }).ToList()
            }).ToList();
        }
    }
}
