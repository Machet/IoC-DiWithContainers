using IoCCinema.DataAccess.Presentation;
using IoCCinema.Presentation;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        public static void Setup()
        {
            var container = new Container();

            container.Register<IMovieViewRepository, EfMovieViewRepository>();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}