[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IoCCinema.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(IoCCinema.App_Start.NinjectWebCommon), "Stop")]

namespace IoCCinema.App_Start
{
    using Business.Authentication;
    using Business.Commands;
    using CompositionRoot;
    using DataAccess;
    using DataAccess.Business;
    using DataAccess.Presentation;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using Presentation;
    using System;
    using System.Web;

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
            kernel.Bind<CinemaContext>().ToSelf().InRequestScope();
            kernel.Bind<ICurrentUserProvider>().To<ContextUserProvider>().InSingletonScope();

            kernel.Bind(x => x.FromAssemblyContaining<CinemaContext>()
                .SelectAllClasses()
                .EndingWith("Repository")
                .BindSingleInterface());

            kernel.Bind<ICommandHandler<LoginCommand>>().To<LoginCommandHandler>()
                .WhenInjectedInto<TransactionalCommandHandler<LoginCommand>>();

            kernel.Bind<ICommandHandler<LoginCommand>>().To<TransactionalCommandHandler<LoginCommand>>();
        }
    }
}
