namespace MyWebApi.Builders.Created
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Contracts.Created;
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
            throw new NotImplementedException();
        }

        public IAndCreatedTestBuilder WithContentNegotiatorOfType<TContentNegotiator>() where TContentNegotiator : IContentNegotiator
        {
            throw new NotImplementedException();
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

        private IContentNegotiator GetContentNegotiator()
        {
            return this.GetActionResultAsDynamic().ContentNegotiator as IContentNegotiator;
        }
    }
}
