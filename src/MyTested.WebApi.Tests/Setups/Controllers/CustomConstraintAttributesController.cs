namespace MyTested.WebApi.Tests.Setups.Controllers
{
    using System.Web.Http;

    public class CustomConstraintAttributesController: ApiController
    {

        [HttpGet]
        [Route("api/testcustomconstraint/custom/{id:custom}")]
        public IHttpActionResult WithAttributesAndParameters(int id)
        {
            return this.Ok();
        }
    }
}
