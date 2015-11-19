using System;

namespace IoCCinema.Presentation
{
    public interface ILoginViewRepository
    {
        LoginAttemptDTO GetLoginAttemptById(Guid id);
    }
}
