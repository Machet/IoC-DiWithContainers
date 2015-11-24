using IoCCinema.Business;
using IoCCinema.Business.DomainEvents;
using IoCCinema.DataAccess.AuditLogging;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace IoCCinema.CompositionRoot
{
    public class UnityDomainEventBus : DomainEventBus
    {
        private readonly IUnityContainer _container;

        public UnityDomainEventBus(IUnityContainer container)
        {
            _container = container;
        }

        protected override IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>()
        {
            foreach (var handler in _container.ResolveAll<IDomainEventHandler<T>>())
            {
                yield return handler.GetType().Name.Contains("Audit")
                    ? handler
                    : new AuditingEventHandler<T>(handler, _container.Resolve<AuditLogger>());
            }
        }
    }
}