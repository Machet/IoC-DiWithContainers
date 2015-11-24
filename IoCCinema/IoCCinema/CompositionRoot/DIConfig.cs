using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.Business.Lotery;
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
            container.RegisterType<IWinChanceCalculatorFactory, UnityWinChanceCalculatorFactory>(new PerRequestLifetimeManager());

            container.RegisterTypes(
                AllClasses.FromAssemblies(businessAssembly)
                    .Where(t => t.GetInterfaces().Any(i => i == typeof(IWinChanceCalculator))),
                WithMappings.FromAllInterfaces,
                (Type t) => t.Name.Replace("UserWinChanceCalculator", string.Empty));

            container.RegisterTypes(
                AllClasses.FromAssemblies(dataAccessAssembly).Where(t => t.Name.Contains("Repository")),
                WithMappings.FromAllInterfaces,
                WithName.Default,
                WithLifetime.Custom<PerRequestLifetimeManager>);

            container.RegisterTypes(
                AllClasses.FromAssemblies(businessAssembly)
                    .Where(t => t.Name.Contains("CommandHandler")),
                WithMappings.FromAllInterfaces,
                (Type t) => "default",
                WithLifetime.Transient);

            container.RegisterType(typeof(ICommandHandler<>), typeof(TransactionalCommandHandler<>),
                new InjectionConstructor(
                    new ResolvedParameter(typeof(ICommandHandler<>), "default"),
                    new ResolvedParameter<CinemaContext>()));

            return container;
        }
    }
}