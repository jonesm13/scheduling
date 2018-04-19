namespace Domain.Features.Template
{
    using System.Collections;
    using System.Threading.Tasks;
    using System.Web.Http;
    using MediatR;

    [RoutePrefix("templates")]
    public class TemplateController : ApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Index(Index.Query query)
        {
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Create(Create.Command command)
        {
        }
    }

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