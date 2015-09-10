namespace MyWebApi.Builders.Contracts.Created
{
    using System.Net.Http.Formatting;

    public interface IFormattersBuilder
    {
        IAndFormattersBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);
    }
}
