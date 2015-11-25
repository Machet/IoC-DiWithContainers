using Autofac;
using IoCCinema.Controllers;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Presentation;
using IoCCinema.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        public static void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CinemaContext>();

            builder.RegisterType<HomeController>();
            builder.RegisterType<EfMovieViewRepository>().As<IMovieViewRepository>();

            IContainer container = builder.Build();
            ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(container));
        }
    }
}