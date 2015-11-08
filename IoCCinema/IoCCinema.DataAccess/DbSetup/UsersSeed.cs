using IoCCinema.Business;

namespace IoCCinema.DataAccess.DbSetup
{
    public class UsersSeed
    {
        public static void Seed(CinemaContext context)
        {
            context.Users.Add(new User { Id = 1, Name = "User1" });
            context.Users.Add(new User { Id = 2, Name = "User2" });
            context.Users.Add(new User { Id = 3, Name = "User3" });
            context.Users.Add(new User { Id = 4, Name = "User4" });
            context.Users.Add(new User { Id = 5, Name = "User5" });
            context.Users.Add(new User { Id = 6, Name = "User6" });
            context.Users.Add(new User { Id = 7, Name = "User7" });
            context.Users.Add(new User { Id = 8, Name = "User8" });

            context.SaveChanges();
        }
    }
}