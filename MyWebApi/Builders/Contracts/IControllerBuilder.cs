namespace MyWebApi.Builders.Contracts
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Http;

    public interface IControllerBuilder<TController>
        where TController : ApiController
    {
        /// <summary>
        /// Indicates which action should be invoked and tested
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action</param>
        /// <returns>Builder for testing the action result</returns>
        IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested
        /// </summary>
        /// <typeparam name="TActionResult">Asynchronous Task result from action</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action</param>
        /// <returns>Builder for testing the action result</returns>
        IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall);
    }
}
