namespace DataModel.Entities
{
    using System;

    public class ScheduleOverride : IEntity
    {
        public Guid Id { get; set; }
        public Guid StationId { get; set; }
        public Guid TemplateId { get; set; }
        public DateTime Date { get; set; }
        public int Start { get; set; }

        public virtual Station Station { get; set; }
        public virtual Template Template { get; set; }
    }
}