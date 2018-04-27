namespace Domain.Features.Station
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using DataModel.Entities;
    using Infrastructure.EntityFramework;
    using MediatR;

    public class Index
    {
        public class Query : IRequest<IEnumerable<Model>>
        {
        }

        public class Handler : QueryHandler<Query, IEnumerable<Model>, SchedulingDbContext>
        {
            public Handler(SchedulingDbContext db) : base(db)
            {
            }

            protected override async Task<IEnumerable<Model>> HandleCore(Query request)
            {
                return await SetAsNoTracking<Station>()
                    .Select(x => new Model
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .ToListAsync();
            }
        }

        public class Model
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}