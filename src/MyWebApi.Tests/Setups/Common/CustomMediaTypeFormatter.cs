namespace MyWebApi.Tests.Setups.Common
{
    using System;
    using System.Net.Http.Formatting;

    public class CustomMediaTypeFormatter : MediaTypeFormatter
    {
        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }
    }
}
