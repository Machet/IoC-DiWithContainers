using IoCCinema.Business;
using IoCCinema.Business.DomainEvents;
using SimpleInjector;
using System.Collections.Generic;

namespace IoCCinema.CompositionRoot
{
    public class SimpleInjectorEventBus : DomainEventBus
    {
        private readonly Container _container;

        public SimpleInjectorEventBus(Container container)
        {
            _container = container;
        }

        protected override IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>()
        {
            return _container.GetAllInstances<IDomainEventHandler<T>>();
        }
    }
}