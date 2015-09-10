namespace MyWebApi.Builders.Contracts.Created
{
    public interface IUriTestBuilder
    {
        IAndUriTestBuilder WithHost(string host);

        IAndUriTestBuilder WithPort(int port);

        IAndUriTestBuilder WithAbsolutePath(string absolutePath);

        IAndUriTestBuilder WithScheme(string scheme);

        IAndUriTestBuilder WithQuery(string query);

        IAndUriTestBuilder WithFragment(string fragment);
    }
}
