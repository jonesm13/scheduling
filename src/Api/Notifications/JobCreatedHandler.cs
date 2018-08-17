namespace Api.Notifications
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Features.Job;
    using Hangfire;
    using MediatR;
    using Scheduling;

    public sealed class JobCreatedHandler : INotificationHandler<Create.JobCreated>
    {
        public Task Handle(Create.JobCreated notification, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue<Scheduler>(x => x.Go(notification.JobId));

            return Task.CompletedTask;
        }
    }
}