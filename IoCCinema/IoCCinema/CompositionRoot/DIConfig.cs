using Castle.MicroKernel.Registration;
using Castle.Windsor;
using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.Controllers;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Business;
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
            container.Register(Component.For<HomeController>().LifeStyle.Transient);
            container.Register(Component.For<LoginController>().LifeStyle.Transient);
            container.Register(Component.For<StringHasher>().LifeStyle.Singleton);
            container.Register(Component.For<ICurrentUserProvider>().ImplementedBy<ContextUserProvider>().LifeStyle.Singleton);

            container.Register(Component.For<CinemaContext>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ILoginViewRepository>().ImplementedBy<EfLoginRepository>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IMovieViewRepository>().ImplementedBy<EfMovieViewRepository>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IAuthenticationRepository>().ImplementedBy<EfAuthenticationRepository>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ICommandHandler<LoginCommand>>().ImplementedBy<TransactionalCommandHandler<LoginCommand>>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ICommandHandler<LoginCommand>>().ImplementedBy<LoginCommandHandler>().LifeStyle.PerWebRequest);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}