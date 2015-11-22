using Castle.Windsor;
using IoCCinema.Business;
using IoCCinema.Business.DomainEvents;
using IoCCinema.DataAccess.AuditLogging;
using System.Collections.Generic;
using System.Linq;

namespace IoCCinema.CompositionRoot
{
    public class WindsorDomainBus : DomainEventBus
    {
        private readonly IWindsorContainer _container;

        public WindsorDomainBus(IWindsorContainer container)
        {
            _container = container;
        }

        protected override IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>()
        {
            var logger = _container.Resolve<AuditLogger>();
            foreach (var handler in _container.ResolveAll<IDomainEventHandler<T>>()
                .OrderBy(h => h.GetType().Name))
            {
                if (handler.GetType().Name.Contains("Audit"))
                {
                    yield return handler;
                }
                else
                {
                    yield return new AuditingEventHandler<T>(handler, logger);
                }
            };
        }
    }
}