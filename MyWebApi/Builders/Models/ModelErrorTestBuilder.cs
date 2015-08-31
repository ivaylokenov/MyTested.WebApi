namespace MyWebApi.Builders.Models
{
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using Base;
    using Contracts.Base;
    using Contracts.Models;

    /// <summary>
    /// Used for testing the model errors.
    /// </summary>
    public class ModelErrorTestBuilder : BaseTestBuilder, IModelErrorTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="modelState">Optional model state dictionary to use the class with. Default is controller's model state.</param>
        public ModelErrorTestBuilder(ApiController controller, string actionName, ModelStateDictionary modelState = null)
            : base(controller, actionName)
        {
            this.ModelState = modelState ?? controller.ModelState;
        }

        /// <summary>
        /// Gets validated model state of the provided ASP.NET Web API controller instance.
        /// </summary>
        /// <value>Model state dictionary containing all validation errors.</value>
        protected ModelStateDictionary ModelState { get; private set; }

        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder ContainingNoModelStateErrors()
        {
            this.CheckValidModelState();
            return this.NewAndProvideTestBuilder();
        }
    }
}
