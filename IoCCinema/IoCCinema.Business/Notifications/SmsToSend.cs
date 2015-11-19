using System;

namespace IoCCinema.Business.Notifications
{
    public class SmsToSend
    {
        public int SmsToSendId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string PhoneNumber { get; set; }
        public bool HasBeenSent { get; set; }
        public DateTime CreationTime { get; set; }
    }
}