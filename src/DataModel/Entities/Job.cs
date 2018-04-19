namespace DataModel.Entities
{
    using System;

    public class Job : IEntity
    {
        public Guid Id { get; set; }
        public Guid StationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public JobState State { get; set; }

        public virtual Station Station { get; set; }
    }

    public enum JobState
    {
        None,
        InProgress,
        Complete
    }
}