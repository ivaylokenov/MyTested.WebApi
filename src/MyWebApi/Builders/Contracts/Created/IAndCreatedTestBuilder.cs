namespace MyWebApi.Builders.Contracts.Created
{
    public interface IAndCreatedTestBuilder : ICreatedTestBuilder
    {
        ICreatedTestBuilder AndAlso();
    }
}
