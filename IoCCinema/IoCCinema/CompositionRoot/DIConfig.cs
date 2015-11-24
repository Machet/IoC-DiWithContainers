using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Presentation;
using IoCCinema.Presentation;
using Microsoft.Practices.Unity;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        public static UnityContainer Setup()
        {
            var container = new UnityContainer();
            container.RegisterType<CinemaContext>(new PerRequestLifetimeManager());
            container.RegisterType<IMovieViewRepository, EfMovieViewRepository>(new PerRequestLifetimeManager());
            return container;
        }
    }
}