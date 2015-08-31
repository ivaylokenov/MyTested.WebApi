namespace MyWebApi.Builders.Contracts.Base
{
    using System.Web.Http;

    /// <summary>
    /// Base interface for all test builders.
    /// </summary>
    public interface IBaseTestBuilder
    {
        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <value>Action name to be tested.</value>
        string ActionName { get; }

        /// <summary>
        /// Gets the controller on which the action is tested.
        /// </summary>
        /// <returns>ASP.NET Web API controller on which the action is tested.</returns>
        ApiController AndProvideTheControllerInstance();
    }
}
