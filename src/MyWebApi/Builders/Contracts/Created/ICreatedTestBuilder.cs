namespace MyWebApi.Builders.Contracts.Created
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Models;

    public interface ICreatedTestBuilder : IBaseResponseModelTestBuilder
    {
        IAndCreatedTestBuilder WithDefaultContentNegotiator();

        IAndCreatedTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator);

        IAndCreatedTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
            where TContentNegotiator : IContentNegotiator;

        IAndCreatedTestBuilder AtLocation(string location);

        IAndCreatedTestBuilder AtLocation(Uri location);

        IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> location);

        IAndCreatedTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);

        IAndCreatedTestBuilder ContainingDefaultFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters);

        IAndCreatedTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters);

        IAndCreatedTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters);

        IAndCreatedTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder);
    }
}
