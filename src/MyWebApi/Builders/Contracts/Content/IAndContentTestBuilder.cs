namespace MyWebApi.Builders.Contracts.Content
{
    public interface IAndContentTestBuilder : IContentTestBuilder
    {
        IContentTestBuilder AndAlso();
    }
}
