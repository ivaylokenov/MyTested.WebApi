namespace MyWebApi.Builders.Contracts.Created
{
    public interface IAndUriTestBuilder : IUriTestBuilder
    {
        IUriTestBuilder AndAlso();
    }
}
