namespace SourceControlSystem.Api.Tests.IntegrationTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Common.Constants;
    using Models.Commits;
    using MyTested.WebApi;
    using Services.Data.Contracts;

    [TestClass]
    public class CommitsIntegrationTests
    {
        [TestInitialize]
        public void Init()
        {
            WebApiConfig.DependencyRegistrationAction = builder =>
            {
                builder.RegisterApiControllers(Assembly.Load(Assemblies.WebApi));
                builder.RegisterInstance(TestObjectFactory.GetCommitsService()).As<ICommitsService>();
            };
        }

        [TestMethod]
        public void ByProjectShouldReturnCorrectResponse()
        {
            MyWebApi
                .Server()
                .Working<Startup>()
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("api/Commits/ByProject/1")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .WithResponseModelOfType<List<ListedCommitResponseModel>>()
                .Passing(m => m.Count == 1);
        }
    }
}
