namespace Domain.Features.Template
{
    using System.Threading.Tasks;
    using System.Web.Http;

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
}