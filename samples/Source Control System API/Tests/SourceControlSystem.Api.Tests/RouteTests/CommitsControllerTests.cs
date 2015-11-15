namespace SourceControlSystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Controllers;
    using MyTested.WebApi;

    [TestClass]
    public class CommitsControllerTests
    {
        [TestMethod]
        public void ByProjectIdShouldMapToCorrectAction()
        {
            MyWebApi
                .Routes()
                .ShouldMap("/api/Commits/ByProject/1")
                .To<CommitsController>(c => c.GetByProjectId(1));
        }
    }
}
