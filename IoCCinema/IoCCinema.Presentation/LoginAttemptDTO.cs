using System;

namespace IoCCinema.Presentation
{
    public class LoginAttemptDTO
    {
        public Guid LoginAttemptId { get; set; }
        public bool Succeeded { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
