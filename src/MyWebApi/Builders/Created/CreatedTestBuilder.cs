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

    /// <summary>
    /// Used for testing created results.
    /// </summary>
    /// <typeparam name="TCreatedResult">Type of created result - CreatedNegotiatedContentResult{T} or CreatedAtRouteNegotiatedContentResult{T}.</typeparam>
    public class CreatedTestBuilder<TCreatedResult>
        : BaseResponseModelTestBuilder<TCreatedResult>, IAndCreatedTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedTestBuilder{TCreatedResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public CreatedTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TCreatedResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether created result has the default content negotiator.
        /// </summary>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder WithDefaultContentNegotiator()
        {
            return this.WithContentNegotiatorOfType<DefaultContentNegotiator>();
        }

        /// <summary>
        /// Tests whether created result has specific type of content negotiator.
        /// </summary>
        /// <param name="contentNegotiator">Expected IContentNegotiator.</param>
        /// <returns>The same created test builder.</returns>
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

        /// <summary>
        /// Tests whether created result has specific type of content negotiator by using generic definition.
        /// </summary>
        /// <typeparam name="TContentNegotiator">Type of IContentNegotiator.</typeparam>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
            where TContentNegotiator : IContentNegotiator
        {
            return this.WithContentNegotiator(Activator.CreateInstance<TContentNegotiator>());
        }

        /// <summary>
        /// Tests whether created result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same created test builder.</returns>
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

        /// <summary>
        /// Tests whether created result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same created test builder.</returns>
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

        /// <summary>
        /// Tests whether created result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same created test builder.</returns>
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

        /// <summary>
        /// Tests whether created result contains the provided media type formatter.
        /// </summary>
        /// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        /// <returns>The same created test builder.</returns>
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

        /// <summary>
        /// Tests whether created result contains the provided type of media type formatter.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new()
        {
            return this.ContainingMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        }

        /// <summary>
        /// Tests whether created result contains the default media type formatters provided by the framework.
        /// </summary>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder ContainingDefaultFormatters()
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

        /// <summary>
        /// Tests whether created result contains exactly the same types of media type formatters as the provided collection.
        /// </summary>
        /// <param name="mediaTypeFormatters">Expected collection of media type formatters.</param>
        /// <returns>The same created test builder.</returns>
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

        /// <summary>
        /// Tests whether created result contains exactly the same types of media type formatters as the provided parameters.
        /// </summary>
        /// <param name="mediaTypeFormatters">Expected collection of media type formatters provided as parameters.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters)
        {
            return this.ContainingMediaTypeFormatters(mediaTypeFormatters.AsEnumerable());
        }

        /// <summary>
        /// Tests whether created result contains the media type formatters provided by builder.
        /// </summary>
        /// <param name="formattersBuilder">Builder for expected media type formatters.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder)
        {
            var newFormattersBuilder = new FormattersBuilder();
            formattersBuilder(newFormattersBuilder);
            var expectedFormatters = newFormattersBuilder.GetMediaTypeFormatters();
            return this.ContainingMediaTypeFormatters(expectedFormatters);
        }

        /// <summary>
        /// AndAlso method for better readability when chaining created tests.
        /// </summary>
        /// <returns>The same created test builder.</returns>
        public ICreatedTestBuilder AndAlso()
        {
            return this;
        }

        private static IList<string> SortMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        {
            return mediaTypeFormatters
                .OrderBy(m => m.GetType().FullName)
                .Select(m => m.GetType().Name)
                .ToList();
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
