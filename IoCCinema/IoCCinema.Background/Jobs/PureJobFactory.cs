using Quartz.Spi;
using Quartz;
using System;

namespace IoCCinema.Background.Jobs
{
    public class PureJobFactory : IJobFactory
    {
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            if (bundle.JobDetail.JobType == typeof(Job1))
            {
                return new Job1();
            }

            if (bundle.JobDetail.JobType == typeof(Job2))
            {
                return new Job2();
            }

            throw new InvalidOperationException("Not supported job type");
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
