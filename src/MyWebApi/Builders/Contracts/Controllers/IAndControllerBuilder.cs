namespace MyWebApi.Builders.Contracts.Controllers
{
    using System.Web.Http;

    /// <summary>
    /// Used for adding AndAlso() method to controller builder.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
    public interface IAndControllerBuilder<TController> : IControllerBuilder<TController>
        where TController : ApiController
    {
        /// <summary>
        /// AndAlso method for better readability when building controller instance.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> AndAlso();
    }
}
