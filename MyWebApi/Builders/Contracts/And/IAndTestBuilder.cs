namespace MyWebApi.Builders.Contracts.And
{
    using Actions;

    /// <summary>
    /// Class containing AndAlso() method allowing additional assertions after model state tests.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IAndTestBuilder<out TActionResult>
    {
        /// <summary>
        /// Method allowing additional assertions after the model state tests.
        /// </summary>
        /// <returns>Builder for testing the action result.</returns>
        IActionResultTestBuilder<TActionResult> AndAlso();
    }
}
