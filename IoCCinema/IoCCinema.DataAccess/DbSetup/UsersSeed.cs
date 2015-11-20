using IoCCinema.Business;
using IoCCinema.Business.Authentication;

namespace IoCCinema.DataAccess.DbSetup
{
    public class UsersSeed
    {
        public static void Seed(CinemaContext context)
        {
            var hasher = new StringHasher();
            for (int i = 1; i <= 10; i++)
            {
                context.Users.Add(new User
                {
                    Id = 1,
                    Name = "User" + i,
                    Email = i + "@movie.com",
                    ContactByEmailAllowed = true,
                    MobilePhone = "" + i + i + i + i + i,
                    ContactBySmslAllowed = true,
                    Password = hasher.GetHash("123", "user" + i),
                    PasswordSalt = "user" + i,
                    UserType = (UserType)(i % 3)
                });
            }

            context.SaveChanges();
        }
    }
}