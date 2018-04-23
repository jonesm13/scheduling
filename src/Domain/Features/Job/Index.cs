namespace Domain.Features.Job
{
    using System.Collections.Generic;
    using MediatR;

    public class Index
    {
        public class Query : IRequest<IEnumerable<Model>>
        {
        }

        public class Model
        {
        }
    }
}