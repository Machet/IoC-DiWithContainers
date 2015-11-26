using IoCCinema.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IoCCinema.Business.DomainEvents;
using Autofac;
using System.Web.Mvc;

namespace IoCCinema.CompositionRoot
{
    public class AutofacDomainEventBus : DomainEventBus
    {
        protected override IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>()
        {
            return DependencyResolver.Current.GetServices<IDomainEventHandler<T>>();
        }
    }
}