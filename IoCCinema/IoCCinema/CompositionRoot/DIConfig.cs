using Castle.MicroKernel.Registration;
using Castle.Windsor;
using IoCCinema.Controllers;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Presentation;
using IoCCinema.Presentation;
using System.Web.Mvc;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        internal static void Setup()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<CinemaContext>().LifeStyle.PerWebRequest);
            container.Register(Component.For<HomeController>().LifeStyle.Transient);
            container.Register(Component.For<IMovieViewRepository>().ImplementedBy<EfMovieViewRepository>().LifeStyle.PerWebRequest);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}