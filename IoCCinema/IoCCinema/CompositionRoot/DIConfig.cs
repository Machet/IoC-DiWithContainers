using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Business;
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
            container.RegisterType<ICurrentUserProvider, ContextUserProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<CinemaContext>(new PerRequestLifetimeManager());
            container.RegisterType<IMovieViewRepository, EfMovieViewRepository>(new PerRequestLifetimeManager());
            container.RegisterType<ILoginViewRepository, EfLoginRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationRepository, EfAuthenticationRepository>(new PerRequestLifetimeManager());

            container.RegisterType<ICommandHandler<LoginCommand>, LoginCommandHandler>("default");
            container.RegisterType<ICommandHandler<LoginCommand>, TransactionalCommandHandler<LoginCommand>>(
                new InjectionConstructor(
                    new ResolvedParameter(typeof(ICommandHandler<LoginCommand>), "default"),
                    new ResolvedParameter<CinemaContext>()));

            return container;
        }
    }
}