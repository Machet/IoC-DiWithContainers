using IoCCinema.Business;
using System;

namespace IoCCinema.Test
{
    public class DomainTimeScope : IDisposable
    {
        private bool _isDisposed;
        private DomainTime _defaultTime;

        public DomainTimeScope(DomainTime newTime)
        {
            _defaultTime = DomainTime.Current;
            DomainTime.Current = newTime;
            _isDisposed = false;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                DomainTime.Current = _defaultTime;
                _isDisposed = true;
            }
        }
    }
}
