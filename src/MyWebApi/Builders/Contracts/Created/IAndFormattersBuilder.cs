namespace MyWebApi.Builders.Contracts.Created
{
    /// <summary>
    /// Used for adding AndAlso() method to the the formatter tests.
    /// </summary>
    public interface IAndFormattersBuilder : IFormattersBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining formatters tests.
        /// </summary>
        /// <returns>The same formatters test builder.</returns>
        IFormattersBuilder AndAlso();
    }
}
