using System;

namespace IoCCinema.Presentation
{
    public interface ILoginRepository
    {
        LoginAttemptDTO GetLoginAttemptById(Guid id);
    }
}
