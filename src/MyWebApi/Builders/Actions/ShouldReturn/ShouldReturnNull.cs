namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using Common.Extensions;
    using Contracts.Base;
    using Exceptions;

    /// <summary>
    /// Class containing methods for testing BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        public IBaseTestBuilderWithActionResult<TActionResult> ShouldReturnNull()
        {
            if (this.ActionResult.Equals(null))
            {
                throw new HttpActionResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected action result to be null, but instead received {2}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    this.ActionResult.GetName()));
            }

            return this.NewAndProvideTestBuilder();
        }
    }
}
