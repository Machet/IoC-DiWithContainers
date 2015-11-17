using IoCCinema.Business.Notifications;
using Quartz;
using System;
using System.Collections.Generic;

namespace IoCCinema.Background.Jobs
{
    public class EmailSendingJob : IJob
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMailSender _mailSender;

        public EmailSendingJob(INotificationRepository notificationRepository, IMailSender mailSender)
        {
            _notificationRepository = notificationRepository;
            _mailSender = mailSender;
        }

        public void Execute(IJobExecutionContext context)
        {
            List<MailToSend> mailsToSend = _notificationRepository.GetUnsentMails();
            foreach(var mail in mailsToSend)
            {
                _mailSender.SendMail(mail);
                mail.HasBeenSent = true;
            }
        }
    }
}
