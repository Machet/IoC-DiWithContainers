using System;
using System.Collections.Generic;

namespace IoCCinema.Presentation
{
    public interface IMovieViewRepository
    {
        List<MovieDTO> GetMovies(DateTime start);
        RoomDTO GetRoomBySeanse(int seanseId);
    }
}
