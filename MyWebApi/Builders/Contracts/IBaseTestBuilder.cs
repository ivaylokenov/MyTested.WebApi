namespace MyWebApi.Builders.Contracts
{
    using System.Web.Http;

    /// <summary>
    /// Base interface for all test builders.
    /// </summary>
    public interface IBaseTestBuilder
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
    }
}
