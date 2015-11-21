using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Business;
using IoCCinema.DataAccess.Presentation;
using IoCCinema.Presentation;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        public static void Setup()
        {
            var container = new Container();
            var perRequest = new WebRequestLifestyle();

            container.Register<CinemaContext>(perRequest);
            container.Register<ICurrentUserProvider, ContextUserProvider>(Lifestyle.Singleton);

            container.Register<IMovieViewRepository, EfMovieViewRepository>(perRequest);
            container.Register<ILoginViewRepository, EfLoginRepository>(perRequest);
            container.Register<IAuthenticationRepository, EfAuthenticationRepository>(perRequest);

            container.RegisterDecorator<ICommandHandler<LoginCommand>, TransactionalCommandHandler<LoginCommand>>();
            container.Register<ICommandHandler<LoginCommand>, LoginCommandHandler>();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}