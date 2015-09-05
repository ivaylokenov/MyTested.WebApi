namespace MyWebApi.Builders.Contracts.Json
{
    using System.Text;
    using Models;

    /// <summary>
    /// Used for testing JSON results.
    /// </summary>
    public interface IJsonTestBuilder : IBaseResponseModelTestBuilder
    {
        IAndJsonTestBuilder WithDefaultEncoding();

        IAndJsonTestBuilder WithEncoding(Encoding encoding);
    }
}
