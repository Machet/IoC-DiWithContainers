using System;
using System.Collections.Generic;

namespace IoCCinema.Presentation
{
    public class NotificationsDTO
    {
        public List<Email> Emails { get; set; }
        public List<Sms> Smses { get; set; }

        public NotificationsDTO()
        {
            Emails = new List<Email>();
            Smses = new List<Sms>();
        }

        public class Email
        {
            public string Subject { get; set; }
            public bool HasBeenSent { get; set; }
            public string Content { get; set; }
            public string From { get; set; }
            public DateTime CreationTime { get; set; }
        }

        public class Sms
        {
            public string Message { get; set; }
            public bool HasBeenSent { get; set; }
            public DateTime CreationTime { get; set; }
        }
    }
}