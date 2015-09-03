namespace MyWebApi.Builders.Contracts.Actions
{
    using Base;
    using ExceptionErrors;

    public interface IShouldThrowTestBuilder : IBaseTestBuilder
    {
        IExceptionTestBuilder Exception();

        IHttpResponseExceptionTestBuilder HttpResponseException();
    }
}
