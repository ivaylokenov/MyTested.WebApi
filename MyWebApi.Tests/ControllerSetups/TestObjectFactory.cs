namespace MyWebApi.Tests.ControllerSetups
{
    using Models;

    public static class TestObjectFactory
    {
        public static RequestModel GetValidRequestModel()
        {
            return new RequestModel
            {
                Id = 2,
                Name = "Test"
            };
        }

        public static RequestModel GetRequestModelWithErrors()
        {
            return new RequestModel();
        }
    }
}
