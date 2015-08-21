namespace MyWebApi.Builders.Results
{
    using System;
    using Contracts;

    using Utilities;

    /// <summary>
    /// Used for testing the action result type of test.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult> 
        : BaseTestBuilder<TActionResult>, IActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ActionResultTestBuilder(string actionName, TActionResult actionResult)
            : base(actionName, actionResult)
        {
        }

        private void ValidateActionReturnType(Type actionReturnType, bool canBeAssignable = false, bool allowDifferentGenericTypeDefinitions = false)
        {
            var typeOfActionResult = this.ActionResult.GetType();

            var isAssignableCheck = canBeAssignable && !actionReturnType.IsAssignableFrom(typeOfActionResult);
            var strictlyEqualCheck = !canBeAssignable && typeOfActionResult != actionReturnType;

            var invalid = isAssignableCheck || strictlyEqualCheck;
            if (strictlyEqualCheck)
            {
                var genericTypeDefinitionCheck = allowDifferentGenericTypeDefinitions &&
                          typeOfActionResult.IsGenericType &&
                          actionReturnType.IsAssignableFrom(typeOfActionResult.GetGenericTypeDefinition());

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
                    actionReturnType.Name,
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
