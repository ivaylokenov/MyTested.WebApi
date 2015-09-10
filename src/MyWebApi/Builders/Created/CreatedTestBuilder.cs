namespace MyWebApi.Builders.Created
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using Common;
    using Common.Extensions;
    using Contracts.Created;
    using Exceptions;
    using Models;
    using Utilities;

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
            if (Reflection.AreDifferentTypes(contentNegotiator, actualContentNegotiator))
            {
                this.ThrowNewCreatedResultAssertionException(
                    "IContentNegotiator",
                    string.Format("to be {0}", contentNegotiator.GetName()),
                    string.Format("instead received {0}", actualContentNegotiator.GetName()));
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
            Uri uri;
            var uriCreated = Uri.TryCreate(location, UriKind.RelativeOrAbsolute, out uri);
            if (!uriCreated)
            {
                this.ThrowNewCreatedResultAssertionException(
                    "location",
                    "to be URI valid",
                    string.Format("instead received {0}", location));
            }

            return this.AtLocation(uri);
        }

        public IAndCreatedTestBuilder AtLocation(Uri location)
        {
            var actualLocation = this.GetActionResultAsDynamic().Location as Uri;
            if (actualLocation == null || location != actualLocation)
            {
                this.ThrowNewCreatedResultAssertionException(
                    "location",
                    string.Format("to be {0}", location.OriginalString),
                    string.Format("instead received {0}", actualLocation != null ? actualLocation.OriginalString : null));
            }

            return this;
        }

        public IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder)
        {
            var actualUri = this.GetActionResultAsDynamic().Location as Uri;

            var newUriTestBuilder = new UriTestBuilder();
            uriTestBuilder(newUriTestBuilder);
            var expectedUri = newUriTestBuilder.GetUri();

            var validations = newUriTestBuilder.GetUriValidations();
            this.ValidateUri(
                expectedUri,
                actualUri,
                validations);

            return this;
        }

        public IAndCreatedTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            var formatters = this.GetActionResultAsDynamic().Formatters as IEnumerable<MediaTypeFormatter>;
            if (formatters == null || formatters.All(f => Reflection.AreDifferentTypes(f, mediaTypeFormatter)))
            {
                this.ThrowNewCreatedResultAssertionException(
                    "Formatters",
                    string.Format("to have {0}", mediaTypeFormatter.GetName()),
                    "none was found");
            }

            return this;
        }

        public IAndCreatedTestBuilder ContainingDefaultFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        {
            return this.ContainingMediaTypeFormatters(new List<MediaTypeFormatter>
            {
                new BsonMediaTypeFormatter(),
                new FormUrlEncodedMediaTypeFormatter(),
                new JQueryMvcFormUrlEncodedFormatter(),
                new JsonMediaTypeFormatter(),
                new XmlMediaTypeFormatter()
            });
        }

        public IAndCreatedTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        {
            var formatters = this.GetActionResultAsDynamic().Formatters as IEnumerable<MediaTypeFormatter>;
            var actualMediaTypeFormatters = SortMediaTypeFormatters(formatters);
            var expectedMediaTypeFormatters = SortMediaTypeFormatters(mediaTypeFormatters);

            if (actualMediaTypeFormatters.Count != expectedMediaTypeFormatters.Count)
            {
                 this.ThrowNewCreatedResultAssertionException(
                     "Formatters",
                     string.Format("to be {0}", expectedMediaTypeFormatters.Count),
                     string.Format("instead found {0}", actualMediaTypeFormatters.Count));
            }

            for (int i = 0; i < actualMediaTypeFormatters.Count; i++)
            {
                var actualMediaTypeFormatter = actualMediaTypeFormatters[i];
                var expectedMediaTypeFormatter = expectedMediaTypeFormatters[i];
                if (actualMediaTypeFormatter != expectedMediaTypeFormatter)
                {
                    this.ThrowNewCreatedResultAssertionException(
                        "Formatters",
                        string.Format("to have {0}", expectedMediaTypeFormatters),
                        "none was found");
                }
            }

            return this;
        }

        public IAndCreatedTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters)
        {
            return this.ContainingMediaTypeFormatters(mediaTypeFormatters.AsEnumerable());
        }

        public IAndCreatedTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder)
        {
            throw new NotImplementedException();
        }

        public ICreatedTestBuilder AndAlso()
        {
            return this;
        }

        private void ValidateUri(
            MockedUri expectedUri,
            Uri actualUri,
            IEnumerable<Func<MockedUri, Uri, bool>> validations)
        {
            if (validations.Any(v => !v(expectedUri, actualUri)))
            {
                this.ThrowNewCreatedResultAssertionException(
                    "URI",
                    "to equal the provided one",
                    "was in fact different");
            }
        }

        private static IList<string> SortMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        {
            return mediaTypeFormatters
                .OrderBy(m => m.GetType().FullName)
                .Select(m => m.GetType().Name)
                .ToList();
        }

        private void ThrowNewCreatedResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new CreatedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected created result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
