namespace MyWebApi.Tests.ControllerSetups
{
    using System.Collections.Generic;
    using Models;
    using NUnit.Framework;

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

        public static List<ResponseModel> GetListOfResponseModels()
        {
            return new List<ResponseModel>
            {
                new ResponseModel { Id = 1, Name = "Test" },
                new ResponseModel { Id = 2, Name = "Another Test" }
            };
        }
    }
}
