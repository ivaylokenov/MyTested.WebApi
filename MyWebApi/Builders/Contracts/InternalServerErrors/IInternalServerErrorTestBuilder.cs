namespace MyWebApi.Builders.Contracts.InternalServerErrors
{
    using Base;
    using Exceptions;

    /// <summary>
    /// Used for testing internal server error results.
    /// </summary>
    public interface IInternalServerErrorTestBuilder : IBaseTestBuilder
    {
        IExceptionTestBuilder WithException();
    }
}
