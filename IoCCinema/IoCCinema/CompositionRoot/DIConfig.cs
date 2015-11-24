using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Business;
using IoCCinema.DataAccess.Presentation;
using IoCCinema.Presentation;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        public static UnityContainer Setup()
        {
            var container = new UnityContainer();
            var businessAssembly = typeof(ICommand).Assembly;
            var dataAccessAssembly = typeof(CinemaContext).Assembly;

            container.RegisterType<ICurrentUserProvider, ContextUserProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<CinemaContext>(new PerRequestLifetimeManager());

            container.RegisterTypes(
                AllClasses.FromAssemblies(dataAccessAssembly).Where(t => t.Name.Contains("Repository")),
                WithMappings.FromAllInterfaces,
                WithName.Default,
                WithLifetime.Custom<PerRequestLifetimeManager>);

            container.RegisterType<ICommandHandler<LoginCommand>, LoginCommandHandler>("default");
            container.RegisterType<ICommandHandler<LoginCommand>, TransactionalCommandHandler<LoginCommand>>(
                new InjectionConstructor(
                    new ResolvedParameter(typeof(ICommandHandler<LoginCommand>), "default"),
                    new ResolvedParameter<CinemaContext>()));

            return container;
        }
    }
}