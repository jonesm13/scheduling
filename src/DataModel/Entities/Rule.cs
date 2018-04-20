namespace DataModel.Entities
{
    using System;

    public class Rule : IEntity
    {
        public Guid Id { get; set; }
        public Guid RuleSetId { get; set; }
        public string Name { get; set; }
        public bool IsAddition { get; set; }
        public string ClrType { get; set; }
        public string State { get; set; }

        public virtual RuleSet RuleSet { get; set; }
    }
}