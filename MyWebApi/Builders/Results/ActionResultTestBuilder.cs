namespace MyWebApi.Builders.Results
{
    using System;
    using System.Web.Http;
    using Contracts;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing the action result type of test.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult> 
        : BaseTestBuilderWithActionResult<TActionResult>, IActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ActionResultTestBuilder(ApiController controller, string actionName, TActionResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }

        private void ValidateActionReturnType(Type typeOfExpectedReturnValue, bool canBeAssignable = false, bool allowDifferentGenericTypeDefinitions = false)
        {
            var typeOfActionResult = this.ActionResult.GetType();

            var isAssignableCheck = canBeAssignable && Reflection.AreNotAssignable(typeOfExpectedReturnValue, typeOfActionResult);
            var haveDifferentGenericArguments = false;
            if (isAssignableCheck && allowDifferentGenericTypeDefinitions && Reflection.IsGeneric(typeOfExpectedReturnValue))
            {
                isAssignableCheck = Reflection.AreAssignableByGenericDefinition(typeOfExpectedReturnValue, typeOfActionResult);

                if (!Reflection.IsGenericTypeDefinition(typeOfExpectedReturnValue))
                {
                    haveDifferentGenericArguments = Reflection.HaveDifferentGenericArguments(typeOfExpectedReturnValue, typeOfActionResult);
                }
            }

            var strictlyEqualCheck = !canBeAssignable && Reflection.AreDifferentTypes(typeOfExpectedReturnValue, typeOfActionResult);

            var invalid = isAssignableCheck || strictlyEqualCheck || haveDifferentGenericArguments;
            if (strictlyEqualCheck)
            {
                var genericTypeDefinitionCheck = Reflection.AreAssignableByGenericDefinition(typeOfExpectedReturnValue, typeOfActionResult);

                if (genericTypeDefinitionCheck)
                {
                    invalid = false;
                }
            }

            if (invalid)
            {
                throw new HttpActionResultAssertionException(string.Format(
                    "When calling {0} expected action result to be a {1}, but instead received a {2}.",
                    this.ActionName,
                    typeOfExpectedReturnValue.Name,
                    typeOfActionResult.Name));
            }
        }

        private void ValidateActionReturnType<TExpectedType>(bool canBeAssignable = false, bool allowDifferentGenericTypeDefinitions = false)
        {
            var typeOfResponseData = typeof(TExpectedType);
            this.ValidateActionReturnType(typeOfResponseData, canBeAssignable, allowDifferentGenericTypeDefinitions);
        }
    }
}
