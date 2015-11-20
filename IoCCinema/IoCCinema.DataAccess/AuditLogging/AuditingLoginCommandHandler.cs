using IoCCinema.Business.Commands;

namespace IoCCinema.DataAccess.AuditLogging
{
    public class AuditingLoginCommandHandler : ICommandHandler<LoginCommand>
    {
        private readonly AuditLogger _logger;
        private readonly ICommandHandler<LoginCommand> _innerHandler;

        public AuditingLoginCommandHandler(ICommandHandler<LoginCommand> innerHandler, AuditLogger logger)
        {
            _innerHandler = innerHandler;
            _logger = logger;
        }

        public void Handle(LoginCommand command)
        {
            _logger.LogAction("User logging in");
            _innerHandler.Handle(command);
            _logger.LogAction("User logged in");
        }
    }
}
