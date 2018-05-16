namespace Domain.Ports
{
    using System;
    using System.Threading.Tasks;

    public interface IProcessSchedulingJobs
    {
        Task Go(Guid jobId);
    }
}