namespace MyWebApi.Builders.Redirect
{
    using System;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.Redirect;
    using Contracts.Uri;
    using Exceptions;

    public class RedirectTestBuilder<TRedirectResult>
        : BaseTestBuilderWithActionResult<TRedirectResult>, IRedirectTestBuilder
    {
        public RedirectTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TRedirectResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        public IBaseTestBuilder AtLocation(string location)
        {
            if (!Uri.IsWellFormedUriString(location, UriKind.RelativeOrAbsolute))
            {
                this.ThrowNewRedirectResultAssertionException(
                       "location",
                       "to be URI valid",
                       string.Format("instead received {0}", location));
            }

            var uri = new Uri(location);
            return this.AtLocation(uri);
        }

        public IBaseTestBuilder AtLocation(Uri location)
        {
            var actualLocation = this.GetActionResultAsDynamic().Location as Uri;
            if (location != actualLocation)
            {
                this.ThrowNewRedirectResultAssertionException(
                    "location",
                    string.Format("to be {0}", location.OriginalString),
                    string.Format("instead received {0}", actualLocation.OriginalString));
            }

            return this;
        }

        public IBaseTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder)
        {
            this.ValidateLocation(uriTestBuilder, this.ThrowNewRedirectResultAssertionException);
            return this;
        }

        private void ThrowNewRedirectResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new RedirectResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected redirect result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
