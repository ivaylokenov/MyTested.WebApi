namespace MyWebApi.Common.Extensions
{
    using System.Web.Http;
    using Utilities;

    public static class ApiControllerExtensions
    {
        public static string GetName(this ApiController controller)
        {
            return controller.GetType().ToFriendlyTypeName();
        }
    }
}
