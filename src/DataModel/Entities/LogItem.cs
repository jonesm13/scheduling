namespace DataModel.Entities
{
    using System;

    public class LogItem : IEntity
    {
        public Guid Id { get; set; }
        public Guid StationId { get; set; }
        public LogItemType Type { get; set; }

        public virtual Station Station { get; set; }
    }

    public enum LogItemType
    {
        Audio,
        Comment,
        Command
    }
}