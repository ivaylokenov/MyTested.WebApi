namespace MyWebApi.Common
{
    public class MockedUri
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string AbsolutePath { get; set; }

        public string Scheme { get; set; }

        public string Query { get; set; }

        public string Fragment { get; set; }
    }
}
