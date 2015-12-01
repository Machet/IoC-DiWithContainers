using IoCCinema.Business;
using IoCCinema.Business.DomainEvents;
using Ninject;
using System.Collections.Generic;

namespace IoCCinema.CompositionRoot
{
    public class NinjectDomainEventBus : DomainEventBus
    {
        private readonly IKernel _kernel;

        public NinjectDomainEventBus(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>()
        {
            return _kernel.GetAll<IDomainEventHandler<T>>();
        }
    }
}