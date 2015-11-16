using IoCCinema.Business;

namespace IoCCinema.DataAccess.DbSetup
{
    public class UsersSeed
    {
        public static void Seed(CinemaContext context)
        {
            context.Users.Add(new User { Id = 1, Name = "User1", Email = "1@movie.com", ContactByEmailAllowed = true, MobilePhone = "111111", ContactBySmslAllowed = true });
            context.Users.Add(new User { Id = 2, Name = "User2", Email = "2@movie.com", ContactByEmailAllowed = true, MobilePhone = "222222", ContactBySmslAllowed = true });
            context.Users.Add(new User { Id = 3, Name = "User3", Email = "3@movie.com", ContactByEmailAllowed = true, MobilePhone = "333333", ContactBySmslAllowed = true });
            context.Users.Add(new User { Id = 4, Name = "User4", Email = "4@movie.com", ContactByEmailAllowed = true, MobilePhone = "444444", ContactBySmslAllowed = true });
            context.Users.Add(new User { Id = 5, Name = "User5", Email = "5@movie.com", ContactByEmailAllowed = true, MobilePhone = "555555", ContactBySmslAllowed = true });
            context.Users.Add(new User { Id = 6, Name = "User6", Email = "6@movie.com", ContactByEmailAllowed = true, MobilePhone = "666666", ContactBySmslAllowed = true });
            context.Users.Add(new User { Id = 7, Name = "User7", Email = "7@movie.com", ContactByEmailAllowed = true, MobilePhone = "777777", ContactBySmslAllowed = true });
            context.Users.Add(new User { Id = 8, Name = "User8", Email = "8@movie.com", ContactByEmailAllowed = true, MobilePhone = "888888", ContactBySmslAllowed = true });

            context.SaveChanges();
        }
    }
}