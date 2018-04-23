namespace DataModel.Entities
{
    using System;

    public class Station : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? RuleSetId { get; set; }

        public virtual RuleSet RuleSet { get; set; }
    }
}