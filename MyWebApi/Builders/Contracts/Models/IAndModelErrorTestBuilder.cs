namespace MyWebApi.Builders.Contracts.Models
{
    /// <summary>
    /// Used for adding And() method to the the model error tests.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IAndModelErrorTestBuilder<TModel> : IModelErrorTestBuilder<TModel>
    {
        /// <summary>
        /// And method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Model error details test builder.</returns>
        IModelErrorTestBuilder<TModel> And();
    }
}
