using Castle.MicroKernel.Registration;
using Castle.Windsor;
using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.Business.Lotery;
using IoCCinema.Controllers;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Business;
using IoCCinema.DataAccess.Presentation;
using IoCCinema.Presentation;
using System.Web.Mvc;
using System.Linq;
using IoCCinema.DataAccess.AuditLogging;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        internal static void Setup()
        {
            var container = new WindsorContainer();
            var businessAssembly = typeof(ICommandHandler<>).Assembly;
            var dataAccessAssembly = typeof(CinemaContext).Assembly;

            container.Register(Component.For<StringHasher>().LifeStyle.Singleton);
            container.Register(Component.For<ICurrentUserProvider>().ImplementedBy<ContextUserProvider>().LifeStyle.Singleton);
            container.Register(Component.For<IWinChanceCalculatorFactory>().ImplementedBy<WindsorWinChanceCalculatorFactory>());
            container.Register(Component.For<AuditLogger>().LifeStyle.PerWebRequest);

            container.Register(Classes.FromThisAssembly()
                .BasedOn<Controller>()
                .WithService.Self()
                .Configure(c => c.LifestyleTransient()));

            container.Register(Classes.FromAssembly(businessAssembly)
                .BasedOn<IWinChanceCalculator>()
                .WithService.Self()
                .LifestylePerWebRequest()
                .Configure(c => c.Named(c.Implementation.Name.Replace("UserWinChanceCalculator", string.Empty))));

            container.Register(Component.For<CinemaContext>().LifeStyle.PerWebRequest);
            container.Register(Classes.FromAssembly(dataAccessAssembly)
                .Where(t => t.Name.Contains("Repository"))
                .WithService.FirstInterface()
                .Configure(c => c.LifestylePerWebRequest()));

            var commandTypes = businessAssembly.GetTypes()
                .Where(t => !t.IsInterface && typeof(ICommand).IsAssignableFrom(t));

            var specificAuditingCommands = dataAccessAssembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsGenericType && t.Name.Contains("Auditing"))
                .ToList();

            foreach (var commandType in commandTypes)
            {
                var handlerInterface = typeof(ICommandHandler<>).MakeGenericType(new[] { commandType });
                var transactionalHandler = typeof(TransactionalCommandHandler<>).MakeGenericType(new[] { commandType });
                var auditingHandler = typeof(AuditingCommandHandler<>).MakeGenericType(new[] { commandType });

                // transactional handlers
                container.Register(Component.For(handlerInterface)
                    .ImplementedBy(transactionalHandler)
                    .LifeStyle.Transient);

                var specificHandler = specificAuditingCommands
                    .FirstOrDefault(t => t.GetInterfaces().Any(i => i == handlerInterface));

                var toRegister = specificHandler ?? auditingHandler;
                container.Register(Component.For(handlerInterface)
                    .ImplementedBy(toRegister)
                    .LifeStyle.Transient);
            }

            container.Register(Classes.FromAssembly(businessAssembly)
                .BasedOn(typeof(ICommandHandler<>))
                .WithService.FirstInterface()
                .Configure(c => c.LifestyleTransient()));

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}