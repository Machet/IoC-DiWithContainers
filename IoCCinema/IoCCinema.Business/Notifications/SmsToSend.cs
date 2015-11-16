namespace IoCCinema.Business.Notifications
{
    public class SmsToSend
    {
        public int SmsToSendId { get; set; }
        public string Message { get; set; }
        public string Number { get; set; }
        public bool HasBeenSent { get; set; }
    }
}