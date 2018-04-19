namespace DataModel.Entities
{
    using System;

    public class TemplateItem : IEntity
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public TemplateItemType Type { get; set; }
        public int Order { get; set; }
        public string State { get; set; }

        public virtual Template Template { get; set; }
    }
}