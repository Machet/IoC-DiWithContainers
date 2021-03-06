﻿using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IoCCinema.DataAccess.DbSetup;
using IoCCinema.CompositionRoot;
using IoCCinema.Business;
using System;

namespace IoCCinema
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new CinemaDbInitializer());
            ControllerBuilder.Current.SetControllerFactory(new PureControllerFactory());
            DomainEventBus.Current = new PureDomainEventBus();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            PerRequestStore.DisposeCurrent();
        }
    }
}
