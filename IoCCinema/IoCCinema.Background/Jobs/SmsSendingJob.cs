using IoCCinema.Business.Notifications;
using Quartz;
using System.Collections.Generic;

namespace IoCCinema.Background.Jobs
{
    public class SmsSendingJob : IJob
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ISmsSender _smsSender;

        public SmsSendingJob(INotificationRepository notificationRepository, ISmsSender smsSender)
        {
            _notificationRepository = notificationRepository;
            _smsSender = smsSender;
        }

        public void Execute(IJobExecutionContext context)
        {
            List<SmsToSend> smsTosend = _notificationRepository.GetUnsentSmses();
            foreach (var sms in smsTosend)
            {
                _smsSender.SendSms(sms);
                sms.HasBeenSent = true;
            }
        }
    }
}
