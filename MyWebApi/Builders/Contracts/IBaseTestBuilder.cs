namespace MyWebApi.Builders.Contracts
{
    using System.Web.Http;

    /// <summary>
    /// Base interface for all test builders.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IBaseTestBuilder<out TActionResult>
    {
        /// <summary>
        /// Gets the controller on which the action is tested.
        /// </summary>
        /// <value>Controller on which the action is tested.</value>
        ApiController Controller { get; }

        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <value>Action name to be tested.</value>
        string ActionName { get; }

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <value>Action result to be tested.</value>
        TActionResult ActionResult { get; }
    }
}
