using IoCCinema.Background.Jobs;
using Quartz;
using Quartz.Impl;

namespace IoCCinema.Background
{
    public class BackgroundService
    {
        private readonly IScheduler _scheduler;

        public BackgroundService()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
            _scheduler.JobFactory = new PureJobFactory();
            Configuration.SetupJobs(_scheduler);
        }

        public void Start()
        {
            _scheduler.Start();
        }

        public void Stop()
        {
            _scheduler.Shutdown();
        }
    }
}
