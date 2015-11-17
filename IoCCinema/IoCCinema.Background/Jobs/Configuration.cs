using Quartz;

namespace IoCCinema.Background.Jobs
{
    public class Configuration
    {
        public static void SetupJobs(IScheduler scheduler)
        {
            IJobDetail job1 = JobBuilder.Create<EmailSendingJob>()
                .Build();

            ITrigger job1Trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(3)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job1, job1Trigger);

            IJobDetail job2 = JobBuilder.Create<SmsSendingJob>()
                .Build();

            ITrigger job2Trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job2, job2Trigger);
        }
    }
}
