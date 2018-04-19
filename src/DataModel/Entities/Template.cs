﻿namespace DataModel.Entities
{
    using System;
    using System.Collections.Generic;

    public class Template : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TemplateItem> Items { get; set; }
    }
}
