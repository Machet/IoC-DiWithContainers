using IoCCinema.Business;
using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.Business.DomainEvents;
using IoCCinema.Business.Lotery;
using IoCCinema.Business.Notifications;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.AuditLogging;
using IoCCinema.DataAccess.Business;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
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

            container.RegisterType(typeof(ICommandHandler<>), typeof(AuditingCommandHandler<>), "audit",
                new InjectionConstructor(
                    new ResolvedParameter(typeof(ICommandHandler<>), "default"),
                    new ResolvedParameter<AuditLogger>()));

            container.RegisterTypes(
                AllClasses.FromAssemblies(dataAccessAssembly)
                    .Where(t => t.Name.StartsWith("Auditing") && t.Name.Contains("CommandHandler") && t.Name != "AuditingCommandHandler"),
                WithMappings.FromAllInterfaces,
                (Type t) => "audit",
                WithLifetime.Transient,
                (Type t) => new[] { new InjectionConstructor(
                    new ResolvedParameter(typeof(ICommandHandler<>).MakeGenericType(t.GetInterfaces().First().GetGenericArguments()), "default"),
                    new ResolvedParameter<AuditLogger>()) });

            container.RegisterType(typeof(ICommandHandler<>), typeof(TransactionalCommandHandler<>),
                new InjectionConstructor(
                new ResolvedParameter(typeof(ICommandHandler<>), "audit"),
                new ResolvedParameter<CinemaContext>()));

            container.RegisterType(typeof(IDomainEventHandler<>), typeof(AuditOccurrenceEventHandler<>), "handler");

            container.RegisterTypes(
               AllClasses.FromAssemblies(businessAssembly)
                   .Where(t => t.GetInterfaces().Any(i => i.Name.Contains("DomainEventHandler"))),
               WithMappings.FromAllInterfaces,
               (Type t) => "default",
               WithLifetime.Custom<PerRequestLifetimeManager>);

            container.RegisterTypes(
               AllClasses.FromAssemblies(businessAssembly)
                   .Where(t => t.GetInterfaces().Any(i => i == typeof(INotificationSender))),
               WithMappings.FromAllInterfaces,
               WithName.TypeName,
               WithLifetime.Custom<PerRequestLifetimeManager>);

            container.RegisterType<List<INotificationSender>>(
                new InjectionFactory(c => c.Resolve<INotificationSender[]>().ToList()));

            DomainEventBus.Current = new UnityDomainEventBus(container);
            return container;
        }
    }
}