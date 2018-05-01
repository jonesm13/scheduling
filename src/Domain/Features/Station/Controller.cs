namespace Domain.Features.Station
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using ApiController = Infrastructure.WebApi.ApiController;

    [RoutePrefix("stations")]
    public class StationController : ApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Index(Index.Query query) => await Ok(Mediator.Send(query ?? new Index.Query()));

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Create(Create.Command command) => await NoContent(Mediator.Send(command));

        [HttpDelete, Route("{stationId}")]
        public async Task<IHttpActionResult> Delete([FromUri] Delete.Command command) => await NoContent(Mediator.Send(command));
    }
}