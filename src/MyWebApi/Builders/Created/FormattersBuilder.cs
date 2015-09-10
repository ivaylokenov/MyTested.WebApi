namespace MyWebApi.Builders.Created
{
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Contracts.Created;

    public class FormattersBuilder : IAndFormattersBuilder
    {
        private readonly ICollection<MediaTypeFormatter> mediaTypeFormatters;

        public FormattersBuilder()
        {
            this.mediaTypeFormatters = new List<MediaTypeFormatter>();
        }

        public IAndFormattersBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            this.mediaTypeFormatters.Add(mediaTypeFormatter);
            return this;
        }

        public IFormattersBuilder AndAlso()
        {
            return this;
        }

        internal ICollection<MediaTypeFormatter> GetMediaTypeFormatters()
        {
            return this.mediaTypeFormatters;
        }
    }
}
