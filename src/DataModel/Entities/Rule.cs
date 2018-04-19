namespace DataModel.Entities
{
    using System;

    public class Rule : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RuleType Type { get; set; }
        public string State { get; set; }
    }

    public enum RuleType
    {
        Addition,
        Subtraction
    }
}