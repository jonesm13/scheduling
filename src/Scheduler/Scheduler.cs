namespace Scheduling
{
    using System;
    using System.Collections.Generic;
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

            IEnumerable<Schedule> schedule = db.Schedule
                .AsNoTracking()
                .Where(x => x.StationId == theJob.StationId)
                .ToList();

            IEnumerable<ScheduleOverride> overrides = db.ScheduleOverride
                .AsNoTracking()
                .Where(x => x.StationId == theJob.StationId)
                .ToList();

            return Task.CompletedTask;
        }
    }
}
