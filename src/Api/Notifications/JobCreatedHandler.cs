namespace Api.Notifications
{
    using System.Threading.Tasks;
    using Domain.Features.Job;
    using Hangfire;
    using MediatR;
    using Scheduling;

    public class JobCreatedHandler : AsyncNotificationHandler<Create.JobCreated>
    {
        protected override Task HandleCore(Create.JobCreated notification)
        {
            BackgroundJob.Enqueue<Scheduler>(x => x.Go(notification.JobId));

            return Task.CompletedTask;
        }
    }
}