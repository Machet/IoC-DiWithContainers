using IoCCinema.Business.DomainEvents;
using System;
using System.Collections.Generic;

namespace IoCCinema.Business
{
    public abstract class DomainEventBus
    {
        private static DomainEventBus _current;

        public static DomainEventBus Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DefaultDomainEventBus();
                }

                return _current;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("DomainEventBus");
                }

                _current = value;
            }
        }

        public void Raise<T>(T @event) where T : IDomainEvent
        {
            IEnumerable<IDomainEventHandler<T>> handlers = GetEventHandlers<T>();
            foreach (var handler in handlers)
            {
                handler.Handle(@event);
            }
        }

        protected abstract IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>() where T : IDomainEvent;
    }

    public class DefaultDomainEventBus : DomainEventBus
    {
        protected override IEnumerable<IDomainEventHandler<T>> GetEventHandlers<T>()
        {
            return new List<IDomainEventHandler<T>>();
        }
    }
}
