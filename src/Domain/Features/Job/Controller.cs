namespace Domain.Features.Job
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using MediatR;

    [RoutePrefix("jobs")]
    public class JobController : ApiController
    {
        readonly IMediator mediator;

        public JobController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Index()
        {
            await mediator.Send(new Index.Query());
        }
    }
}