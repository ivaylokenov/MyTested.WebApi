namespace MyWebApi.Builders.Contracts
{
    using System.Web.Http;

    public interface IControllerBuilder<TController>
        where TController : ApiController
    {
        void Calling<TAction>();
    }
}
