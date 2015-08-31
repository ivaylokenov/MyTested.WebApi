namespace MyWebApi.Common.Extensions
{
    using System.Web.Http;

    using Utilities;

    /// <summary>
    /// Provides extension methods to ASP.NET Web API controller class.
    /// </summary>
    public static class ApiControllerExtensions
    {
        /// <summary>
        /// Gets friendly type name of ASP.NET Web API controller. Useful for generic types.
        /// </summary>
        /// <param name="controller">ASP.NET Web API controller to get friendly name from.</param>
        /// <returns>Friendly name as string.</returns>
        public static string GetName(this ApiController controller)
        {
            return controller.GetType().ToFriendlyTypeName();
        }
    }
}
