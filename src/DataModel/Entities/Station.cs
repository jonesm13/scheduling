namespace DataModel.Entities
{
    using System;

    public class Station : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}