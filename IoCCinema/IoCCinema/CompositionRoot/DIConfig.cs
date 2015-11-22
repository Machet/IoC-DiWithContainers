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

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        internal static void Setup()
        {
            var container = new WindsorContainer();
            var businessAssembly = typeof(ICommandHandler<>).Assembly;
            container.Register(Component.For<StringHasher>().LifeStyle.Singleton);
            container.Register(Component.For<ICurrentUserProvider>().ImplementedBy<ContextUserProvider>().LifeStyle.Singleton);
            container.Register(Component.For<IWinChanceCalculatorFactory>().ImplementedBy<WindsorWinChanceCalculatorFactory>());

            container.Register(Classes.FromThisAssembly()
                .BasedOn<Controller>()
                .WithService.Self()
                .Configure(c => c.LifestyleTransient()));

            container.Register(Classes.FromAssembly(businessAssembly)
                .BasedOn<IWinChanceCalculator>()
                .WithService.Self()
                .Configure(c => c.Named(c.Implementation.Name.Replace("UserWinChanceCalculator", string.Empty))));

            container.Register(Component.For<CinemaContext>().LifeStyle.PerWebRequest);
            container.Register(Classes.FromAssemblyContaining<CinemaContext>()
                .Where(t => t.Name.Contains("Repository"))
                .WithService.FirstInterface()
                .Configure(c => c.LifestylePerWebRequest()));

            var commandTypes = businessAssembly.GetTypes()
                .Where(t => !t.IsInterface && typeof(ICommand).IsAssignableFrom(t));

            foreach(var commandType in commandTypes)
            {
                var handlerInterface = typeof(ICommandHandler<>).MakeGenericType(new[] { commandType });
                var transactionalHandler = typeof(TransactionalCommandHandler<>).MakeGenericType(new[] { commandType });
                container.Register(Component.For(handlerInterface)
                    .ImplementedBy(transactionalHandler)
                    .LifeStyle.PerWebRequest);
            }

            container.Register(Classes.FromAssembly(businessAssembly)
                .BasedOn(typeof(ICommandHandler<>))
                .WithService.FirstInterface()
                .Configure(c => c.LifestyleTransient()));

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}