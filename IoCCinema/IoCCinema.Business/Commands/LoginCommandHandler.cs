using IoCCinema.Business.Authentication;

namespace IoCCinema.Business.Commands
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand>
    {
        private readonly StringHasher _hasher;
        private readonly IAuthenticationRepository _authRepository;

        public LoginCommandHandler(IAuthenticationRepository authRepository, StringHasher hasher)
        {
            _hasher = hasher;
            _authRepository = authRepository;
        }

        public void Handle(LoginCommand command)
        {
            User user = _authRepository.FindUserByUserName(command.Username);
            if (string.IsNullOrEmpty(command.Password) || string.IsNullOrEmpty(command.Username))
            {
                _authRepository.Add(LoginAttempt.Failed(command.AttemptId, "Invalid credentials"));
                return;
            }

            if (user == null)
            {
                _authRepository.Add(LoginAttempt.Failed(command.AttemptId, "User not found"));
                return;
            }

            if (!_hasher.CompareHash(command.Password, user.Password, user.PasswordSalt))
            {
                _authRepository.Add(LoginAttempt.Failed(command.AttemptId, "Invalid password", user));
                return;
            }

            _authRepository.Add(LoginAttempt.Successful(command.AttemptId, user));
        }
    }
}
