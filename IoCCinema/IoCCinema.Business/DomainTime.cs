using System;

namespace IoCCinema.Business
{
    public abstract class DomainTime
    {
        private static DomainTime _current;

        public static DomainTime Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DefaultDomainTime();
                }

                return _current;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("DomainTime");
                }

                _current = value;
            }
        }

        public abstract DateTime Now { get; }
    }

    public class DefaultDomainTime : DomainTime
    {
        public override DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
