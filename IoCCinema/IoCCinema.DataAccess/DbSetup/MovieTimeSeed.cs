using IoCCinema.Business;
using System;

namespace IoCCinema.DataAccess.DbSetup
{
	public class MovieTimeSeed 
	{
		public static void Seed(CinemaContext context)
		{
            context.Seanses.Add(new Seanse { RoomId = 1, MovieId = 1, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 1, MovieId = 1, StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(15, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 1, MovieId = 1, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 1, MovieId = 1, StartTime = new TimeSpan(21, 0, 0), EndTime = new TimeSpan(23, 30, 0) });

            context.Seanses.Add(new Seanse { RoomId = 2, MovieId = 2, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(12, 0, 0) });
            context.Seanses.Add(new Seanse { RoomId = 2, MovieId = 2, StartTime = new TimeSpan(12, 30, 0), EndTime = new TimeSpan(15, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 2, MovieId = 2, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(18, 0, 0) });
            context.Seanses.Add(new Seanse { RoomId = 2, MovieId = 2, StartTime = new TimeSpan(21, 00, 0), EndTime = new TimeSpan(23, 00, 0) });

            context.Seanses.Add(new Seanse { RoomId = 3, MovieId = 3, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(12, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 3, MovieId = 3, StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(16, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 3, MovieId = 3, StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(20, 30, 0) });

            context.Seanses.Add(new Seanse { RoomId = 4, MovieId = 4, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 4, MovieId = 4, StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(15, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 4, MovieId = 4, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(18, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 4, MovieId = 4, StartTime = new TimeSpan(20, 0, 0), EndTime = new TimeSpan(21, 30, 0) });

            context.Seanses.Add(new Seanse { RoomId = 5, MovieId = 5, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 40, 0) });
            context.Seanses.Add(new Seanse { RoomId = 5, MovieId = 5, StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(13, 40, 0) });
            context.Seanses.Add(new Seanse { RoomId = 5, MovieId = 5, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(15, 40, 0) });
            context.Seanses.Add(new Seanse { RoomId = 5, MovieId = 5, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(16, 40, 0) });
            context.Seanses.Add(new Seanse { RoomId = 5, MovieId = 5, StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(18, 40, 0) });

            context.Seanses.Add(new Seanse { RoomId = 6, MovieId = 6, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(12, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 6, MovieId = 6, StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(16, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 6, MovieId = 6, StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(20, 30, 0) });

            context.Seanses.Add(new Seanse { RoomId = 7, MovieId = 7, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 7, MovieId = 7, StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(15, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 7, MovieId = 7, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 7, MovieId = 7, StartTime = new TimeSpan(21, 0, 0), EndTime = new TimeSpan(23, 30, 0) });

            context.Seanses.Add(new Seanse { RoomId = 8, MovieId = 8, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 40, 0) });
            context.Seanses.Add(new Seanse { RoomId = 8, MovieId = 8, StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(15, 40, 0) });
            context.Seanses.Add(new Seanse { RoomId = 8, MovieId = 8, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 40, 0) });
            context.Seanses.Add(new Seanse { RoomId = 8, MovieId = 8, StartTime = new TimeSpan(21, 0, 0), EndTime = new TimeSpan(23, 40, 0) });

            context.Seanses.Add(new Seanse { RoomId = 1, MovieId = 9, StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) });
            context.Seanses.Add(new Seanse { RoomId = 3, MovieId = 9, StartTime = new TimeSpan(21, 0, 0), EndTime = new TimeSpan(23, 30, 0) });

            context.Seanses.Add(new Seanse { RoomId = 2, MovieId = 10, StartTime = new TimeSpan(22, 0, 0), EndTime = new TimeSpan(23, 40, 0) });
            context.Seanses.Add(new Seanse { RoomId = 1, MovieId = 10, StartTime = new TimeSpan(18, 20, 0), EndTime = new TimeSpan(22, 00, 0) });

            context.SaveChanges();
		}
	}
}
