using IoCCinema.Business.Commands;
using Newtonsoft.Json;
using System;

namespace IoCCinema.DataAccess.AuditLogging
{
    public class AuditingCommandHandler<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly AuditLogger _logger;
        private readonly ICommandHandler<T> _innerHandler;

        public AuditingCommandHandler(ICommandHandler<T> innerHandler, AuditLogger logger)
        {
            _innerHandler = innerHandler;
            _logger = logger;
        }

        public void Handle(T command)
        {
            string serializedEvent = JsonConvert.SerializeObject(command);
            _logger.LogAction("User performed " + typeof(T).Name + Environment.NewLine + serializedEvent);
            _innerHandler.Handle(command);
        }
    }
}
