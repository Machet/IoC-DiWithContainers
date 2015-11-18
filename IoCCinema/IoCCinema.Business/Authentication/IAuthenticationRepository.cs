using System;

namespace IoCCinema.Business.Authentication
{
    public interface IAuthenticationRepository
    {
        User FindUserByUserName(string userName);
        LoginAttempt GetLoginAttemptById(Guid id);
        void Add(LoginAttempt loginAttempt);
    }
}
