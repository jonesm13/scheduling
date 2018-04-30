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

        [HttpDelete, Route("{stationId}")]
        public async Task<IHttpActionResult> Delete(Delete.Command command) => await NoContent(Mediator.Send(command));
    }
}