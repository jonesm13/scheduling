namespace DataModel.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class RuleSet : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Rule> Rules { get; set; }
    }
}