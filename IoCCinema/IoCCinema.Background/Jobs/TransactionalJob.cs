using IoCCinema.DataAccess;
using Quartz;

namespace IoCCinema.Background.Jobs
{
    public class TransactionalJob : IJob
    {
        private readonly IJob _innerJob;
        private readonly CinemaContext _context;

        public TransactionalJob(IJob job, CinemaContext context)
        {
            _innerJob = job;
            _context = context;
        }

        public void Execute(IJobExecutionContext context)
        {
            _innerJob.Execute(context);
            _context.SaveChanges();
        }
    }
}
