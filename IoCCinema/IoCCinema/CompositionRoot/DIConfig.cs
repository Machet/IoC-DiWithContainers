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
            container.Register(Component.For<StringHasher>().LifeStyle.Singleton);
            container.Register(Component.For<ICurrentUserProvider>().ImplementedBy<ContextUserProvider>().LifeStyle.Singleton);

            container.Register(Classes.FromThisAssembly()
                .BasedOn<Controller>()
                .WithService.Self()
                .Configure(c => c.LifestyleTransient()));

            container.Register(Component.For<CinemaContext>().LifeStyle.PerWebRequest);
            container.Register(Classes.FromAssemblyContaining<CinemaContext>()
                .Where(t => t.Name.Contains("Repository"))
                .WithService.FirstInterface()
                .Configure(c => c.LifestylePerWebRequest()));

            container.Register(Component.For<ICommandHandler<LoginCommand>>()
                .ImplementedBy<TransactionalCommandHandler<LoginCommand>>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ICommandHandler<LoginCommand>>()
                .ImplementedBy<LoginCommandHandler>().LifeStyle.PerWebRequest);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}