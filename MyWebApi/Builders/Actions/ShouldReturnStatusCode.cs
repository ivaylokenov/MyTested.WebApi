namespace MyWebApi.Builders.Actions
{
    using System.Net;
    using System.Web.Http.Results;

    using Exceptions;
    using Utilities;

    public partial class ActionResultTestBuilder<TActionResult>
    {
        public void ShouldReturnStatusCode()
        {
            this.ShouldReturn<StatusCodeResult>();
        }

        public void ShouldReturnStatusCode(HttpStatusCode statusCode)
        {
            this.ShouldReturnStatusCode();
            var statusCodeResult = this.ActionResult as StatusCodeResult;
            if (statusCodeResult.StatusCode != statusCode)
            {
                throw new HttpStatusCodeAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have {2} ({3}) status code, but received {4} ({5}).",
                    this.ActionName,
                    this.Controller.GetType().ToFriendlyGenericTypeName(),
                    (int)statusCode,
                    statusCode,
                    (int)statusCodeResult.StatusCode,
                    statusCodeResult.StatusCode));
            }
        }
    }
}
