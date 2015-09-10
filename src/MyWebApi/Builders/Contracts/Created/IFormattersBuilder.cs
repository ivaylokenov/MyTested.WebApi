namespace MyWebApi.Builders.Contracts.Created
{
    using System.Net.Http.Formatting;

    /// <summary>
    /// Used for testing media type formatters in a created result.
    /// </summary>
    public interface IFormattersBuilder
    {
        /// <summary>
        /// Tests whether created result contains the provided media type formatter.
        /// </summary>
        /// <param name="mediaTypeFormatter">Instance of MediaTypeFormatter.</param>
        /// <returns>The same formatters test builder.</returns>
        IAndFormattersBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);

        /// <summary>
        /// Tests whether created result contains the provided media type formatter.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Instance of MediaTypeFormatter.</typeparam>
        /// <returns>The same formatters test builder.</returns>
        IAndFormattersBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new();
    }
}
