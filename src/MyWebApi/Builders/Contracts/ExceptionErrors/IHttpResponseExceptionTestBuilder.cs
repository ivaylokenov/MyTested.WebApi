namespace MyWebApi.Builders.Contracts.ExceptionErrors
{
    using System.Net;
    using Base;

    public interface IHttpResponseExceptionTestBuilder : IBaseTestBuilder
    {
        IBaseTestBuilder WithStatusCode(HttpStatusCode statusCode);
    }
}
