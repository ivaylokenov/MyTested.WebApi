namespace MyWebApi.Builders
{
    using System.Web.Http;

    using Base;
    using Contracts;
    using Exceptions;

    /// <summary>
    /// Used for testing the response model errors.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public class ResponseModelErrorTestBuilder<TResponseModel> : BaseTestBuilder, IResponseModelErrorTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelErrorTestBuilder{TResponseModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        public ResponseModelErrorTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
        }

        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        public void ContainingNoErrors()
        {
            if (!this.Controller.ModelState.IsValid)
            {
                throw new ResponseModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected response model to have no errors, but it had some.",
                    this.ActionName,
                    this.Controller.GetType().Name));
            }
        }
    }
}
