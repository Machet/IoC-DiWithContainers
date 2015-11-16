using Quartz;
using System;

namespace IoCCinema.Background.Jobs
{
    public class Job2 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("2");
        }
    }
}
