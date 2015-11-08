using IoCCinema.Business;
using System;

namespace IoCCinema.DataAccess.DbSetup
{
    public class MovieSeed
    {
        public static void Seed(CinemaContext context)
        {
            context.Movies.Add(new Movie
            {
                MovieId = 1,
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                Title = "The Shawshank Redemption",
                Length = TimeSpan.FromMinutes(142)
            });

            context.Movies.Add(new Movie
            {
                MovieId = 2,
                Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                Title = "The Godfather",
                Length = TimeSpan.FromMinutes(175)
            });

            context.Movies.Add(new Movie
            {
                MovieId = 3,
                Description = "The early life and career of Vito Corleone in 1920s New York is portrayed while his son, Michael, expands and tightens his grip on his crime syndicate stretching from Lake Tahoe, Nevada to pre-revolution 1958 Cuba.",
                Title = "The Godfather: Part II",
                Length = TimeSpan.FromMinutes(200)
            });

            context.Movies.Add(new Movie
            {
                MovieId = 4,
                Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, the caped crusader must come to terms with one of the greatest psychological tests of his ability to fight injustice.",
                Title = "The Dark Knight",
                Length = TimeSpan.FromMinutes(152)
            });

            context.Movies.Add(new Movie
            {
                MovieId = 5,
                Description = "A dissenting juror in a murder trial slowly manages to convince the others that the case is not as obviously clear as it seemed in court.",
                Title = "12 Angry Men",
                Length = TimeSpan.FromMinutes(96)
            });

            context.Movies.Add(new Movie
            {
                MovieId = 6,
                Description = "In Poland during World War II, Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                Title = "Schindler's List",
                Length = TimeSpan.FromMinutes(195)
            });

            context.Movies.Add(new Movie
            {
                MovieId = 7,
                Description = "The lives of two mob hit men, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                Title = "Pulp fiction",
                Length = TimeSpan.FromMinutes(154)
            });

            context.Movies.Add(new Movie
            {
                MovieId = 8,
                Title = "Good bad and ugly",
                Description = "A bounty hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.",
                Length = TimeSpan.FromMinutes(161)
            });

            context.Movies.Add(new Movie
            {
                MovieId = 9,
                Title = "The Lord of the Rings: The Return of the King",
                Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                Length = TimeSpan.FromMinutes(201)
            });

            context.Movies.Add(new Movie
            {
                MovieId = 10,
                Title = "Fight Club",
                Description = "An insomniac office worker, looking for a way to change his life, crosses paths with a devil-may-care soap maker, forming an underground fight club that evolves into something much, much more...",
                Length = TimeSpan.FromMinutes(139)
            });

            context.SaveChanges();
        }
    }
}
