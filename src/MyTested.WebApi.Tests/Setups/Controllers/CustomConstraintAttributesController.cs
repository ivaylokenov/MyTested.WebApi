namespace MyTested.WebApi.Tests.Setups.Controllers
{
    using System.Web.Http;

    [RoutePrefix("api/testcustomconstraint")]
    public class CustomConstraintAttributesController: ApiController
    {

        [HttpGet]
        [Route("custom/{id:custom}")]
        public IHttpActionResult WithAttributesAndParameters(int id)
        {
            return this.Ok();
        }
    }
}
