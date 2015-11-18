using System;

namespace IoCCinema.Business.Authentication
{
    public class LoginAttempt
    {
        public Guid LoginAttemptId { get; set; }
        public bool Succeeded { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }

        internal static LoginAttempt Failed(Guid id, string message)
        {
            return new LoginAttempt
            {
                LoginAttemptId = id,
                Succeeded = false,
                Message = message,
                Time = DateTime.Now,
                UserId = null
            };
        }

        internal static LoginAttempt Failed(Guid id, string message, User user)
        {
            return new LoginAttempt
            {
                LoginAttemptId = id,
                Succeeded = false,
                Message = message,
                Time = DateTime.Now,
                UserId = user.Id
            };
        }

        internal static LoginAttempt Successful(Guid id, User user)
        {
            return new LoginAttempt
            {
                LoginAttemptId = id,
                Succeeded = true,
                Message = "Successfull",
                Time = DateTime.Now,
                UserId = user.Id
            };
        }
    }
}
