using IoCCinema.Business;

namespace IoCCinema.DataAccess.DbSetup
{
    public class RoomSeed 
    {
        public static void Seed(CinemaContext context)
        {
            context.Rooms.Add(new Room { Id = 1, RoomNumber = 1, RowsOfSeats = 5, SeatsPerRow = 10 });
            context.Rooms.Add(new Room { Id = 2, RoomNumber = 2, RowsOfSeats = 5, SeatsPerRow = 10 });
            context.Rooms.Add(new Room { Id = 3, RoomNumber = 3, RowsOfSeats = 6, SeatsPerRow = 10 });
            context.Rooms.Add(new Room { Id = 4, RoomNumber = 4, RowsOfSeats = 6, SeatsPerRow = 8 });
            context.Rooms.Add(new Room { Id = 5, RoomNumber = 5, RowsOfSeats = 6, SeatsPerRow = 8 });
            context.Rooms.Add(new Room { Id = 6, RoomNumber = 6, RowsOfSeats = 10, SeatsPerRow = 5 });
            context.Rooms.Add(new Room { Id = 7, RoomNumber = 7, RowsOfSeats = 5, SeatsPerRow = 9 });
            context.Rooms.Add(new Room { Id = 8, RoomNumber = 8, RowsOfSeats = 5, SeatsPerRow = 9 });

            context.SaveChanges();
        }
    }
}
