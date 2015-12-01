[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IoCCinema.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(IoCCinema.App_Start.NinjectWebCommon), "Stop")]

namespace IoCCinema.App_Start
{
    using Business.Authentication;
    using Business.Commands;
    using Business.Lotery;
    using CompositionRoot;
    using DataAccess;
    using DataAccess.Business;
    using DataAccess.AuditLogging;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using System;
    using System.Web;
    using Business;
    using Business.Notifications;
    using Business.DomainEvents;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            var businessAssembly = typeof(ICommand).Assembly;
            var dataAccessAssembly = typeof(CinemaContext).Assembly;

            kernel.Bind<CinemaContext>().ToSelf().InRequestScope();
            kernel.Bind<AuditLogger>().ToSelf().InRequestScope();
            kernel.Bind<ICurrentUserProvider>().To<ContextUserProvider>().InSingletonScope();
            kernel.Bind<IWinChanceCalculatorFactory>().To<NinjectWinChanceCalculatorFactory>();

            kernel.Bind(x => x.From(businessAssembly)
                .SelectAllClasses()
                .InheritedFrom<INotificationSender>()
                .BindSingleInterface());

            kernel.Bind(x => x.From(businessAssembly)
                .SelectAllClasses()
                .InheritedFrom<IWinChanceCalculator>()
                .BindSingleInterface()
                .Configure((r, type) => r.Named(type.Name.Replace("UserWinChanceCalculator", string.Empty))));

            kernel.Bind(x => x.FromAssemblyContaining<CinemaContext>()
                .SelectAllClasses()
                .EndingWith("Repository")
                .BindSingleInterface()
                .Configure(r => r.InRequestScope()));

            kernel.Bind(x => x.From(businessAssembly)
                .SelectAllClasses()
                .EndingWith("CommandHandler")
                .BindSingleInterface()
                .Configure(r => r.WhenParentNamed("auditingCommand")));

            kernel.Bind(x => x.From(dataAccessAssembly)
                .SelectAllClasses()
                .Where(t => !t.IsGenericType && t.Name.Contains("Audit") && t.Name.Contains("CommandHandler"))
                .BindSingleInterface()
                .Configure(r => r.WhenInjectedInto(typeof(TransactionalCommandHandler<>)).Named("auditingCommand")));

            kernel.Bind(typeof(ICommandHandler<>)).To(typeof(AuditingCommandHandler<>))
                .WhenInjectedInto(typeof(TransactionalCommandHandler<>))
                .Named("auditingCommand"); ;

            kernel.Bind(typeof(ICommandHandler<>)).To(typeof(TransactionalCommandHandler<>));

            kernel.Bind(x => x.From(businessAssembly)
                .SelectAllClasses()
                .InheritedFrom(typeof(IDomainEventHandler<>))
                .BindAllInterfaces()
                .Configure(c => c.WhenInjectedInto(typeof(AuditingEventHandler<>)).InRequestScope()));

            kernel.Bind(typeof(IDomainEventHandler<>)).To(typeof(AuditOccurrenceEventHandler<>)).InRequestScope();
            kernel.Bind(typeof(IDomainEventHandler<>)).To(typeof(AuditingEventHandler<>)).InRequestScope();

            DomainEventBus.Current = new NinjectDomainEventBus(kernel);
        }
    }
}
