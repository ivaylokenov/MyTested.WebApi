namespace MyWebApi.Builders.Contracts.And
{
    using System.Web.Http;

    public interface IAndContinuityTestBuilder
    {
        ApiController ProvideTheControllerInstance();
    }
}
