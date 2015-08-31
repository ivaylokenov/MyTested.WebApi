namespace MyWebApi.Builders.Contracts.ExceptionErrors
{
    using Base;

    public interface IExceptionTestBuilder : IBaseTestBuilder
    {
        IExceptionTestBuilder OfType<TException>();

        IExceptionMessageTestBuilder WithMessage();

        IExceptionTestBuilder WithMessage(string message);
    }
}
