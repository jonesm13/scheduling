namespace Domain.Features.Template.Item
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using ApiController = Infrastructure.WebApi.ApiController;

    [RoutePrefix("templates/{templateId}/items")]
    public class TemplateItemController : ApiController
    {
        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Add(Add.Command command) => await NoContent(Mediator.Send(command));

        [HttpDelete, Route("{order}")]
        public async Task<IHttpActionResult> Delete(Delete.Command command) => await NoContent(Mediator.Send(command));
    }
}