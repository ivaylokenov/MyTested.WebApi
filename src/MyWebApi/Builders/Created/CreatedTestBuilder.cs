namespace MyWebApi.Builders.Created
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.Created;
    using Exceptions;
    using Models;

    public class CreatedTestBuilder<TCreatedResult>
        : BaseResponseModelTestBuilder<TCreatedResult>, IAndCreatedTestBuilder
    {
        public CreatedTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TCreatedResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        public IAndCreatedTestBuilder WithDefaultContentNegotiator()
        {
            return this.WithContentNegotiatorOfType<DefaultContentNegotiator>();
        }

        public IAndCreatedTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator)
        {
            var actualContentNegotiator = this.GetActionResultAsDynamic().ContentNegotiator as IContentNegotiator;
            if (!contentNegotiator.Equals(actualContentNegotiator))
            {
                throw new CreatedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected JSON result encoding to be {2}, but instead received {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    contentNegotiator.GetName(),
                    actualContentNegotiator.GetName()));
            }

            return this;
        }

        public IAndCreatedTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
            where TContentNegotiator : IContentNegotiator
        {
            return this.WithContentNegotiator(Activator.CreateInstance<TContentNegotiator>());
        }

        public IAndCreatedTestBuilder AtLocation(string location)
        {
            throw new NotImplementedException();
        }

        public IAndCreatedTestBuilder AtLocation(Uri location)
        {
            throw new NotImplementedException();
        }

        public IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> location)
        {
            throw new NotImplementedException();
        }

        public IAndCreatedTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            throw new NotImplementedException();
        }

        public IAndCreatedTestBuilder ContainingDefaultFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        {
            throw new NotImplementedException();
        }

        public IAndCreatedTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        {
            throw new NotImplementedException();
        }

        public IAndCreatedTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters)
        {
            throw new NotImplementedException();
        }

        public IAndCreatedTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder)
        {
            throw new NotImplementedException();
        }

        public ICreatedTestBuilder AndAlso()
        {
            throw new NotImplementedException();
        }
    }
}
