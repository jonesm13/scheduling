namespace Domain.Features.Station
{
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("stations")]
    public class StationController : ApiController
    {
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Index(Index.Query query)
        {
        }
    }
}