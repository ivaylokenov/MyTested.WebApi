namespace MyWebApi.Builders.Contracts
{
    public interface IAndUnauthorizedResultTestBuilder : IUnauthorizedResultTestBuilder
    {
        IUnauthorizedResultTestBuilder And();
    }
}
