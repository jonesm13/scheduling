namespace Scheduler
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using DataModel.Entities;
    using Domain.Ports;

    public class Scheduler : IProcessSchedulingJobs
    {
        readonly SchedulingDbContext db;

        public Scheduler(SchedulingDbContext db)
        {
            this.db = db;
        }

        public Task Go(Guid jobId)
        {
            Job theJob = db.Jobs
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == jobId);

            if (theJob == null)
            {
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
