using IoCCinema.Business.Notifications;
using System;

namespace IoCCinema.DataAccess.Notifications
{
    public class GateSmsSender : ISmsSender, IDisposable
    {
        private bool _isDisposed;

        public void SendSms(SmsToSend sms)
        {
            // connect to gate
            // prepare sms
            // send
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
            }
        }
    }
}
