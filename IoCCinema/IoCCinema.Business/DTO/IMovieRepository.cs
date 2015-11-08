using System;
using System.Collections.Generic;

namespace IoCCinema.Business.DTO
{
    public interface IMovieRepository
    {
        List<MovieDTO> GetMovies(DateTime start);
    }
}
