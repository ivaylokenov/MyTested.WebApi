namespace SourceControlSystem.Api.Controllers
{
    using Services.Data.Contracts;
    using System.Web.Http;
    using AutoMapper.QueryableExtensions;
    using Models.Commits;
    using System.Linq;
    using System.Net.Http;
    using System.Net;
    using System;

    [RoutePrefix("api/Commits")]
    public class CommitsController : ApiController
    {
        private readonly ICommitsService commits;

        public CommitsController(ICommitsService commitsService)
        {
            this.commits = commitsService;
        }

        [Route("ByProject/{id}")]
        [HttpGet]
        public IHttpActionResult GetByProjectId(int id)
        {
            var result = this
                .commits
                .GetAllByProjectId(id)
                .ProjectTo<ListedCommitResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [Route("UserHasCommits")]
        public HttpResponseMessage UserHasCommits(string username)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (this.Request.Headers.Contains("MyCustomHeader"))
                {
                    var result = this.commits.UserHasCommits(username);

                    var httpResult = this.Request.CreateResponse(HttpStatusCode.Found, result);
                    httpResult.Headers.Location = new Uri("http://telerikacademy.com");

                    return httpResult;
                }

                return this.Request.CreateResponse(HttpStatusCode.NoContent);
            }

            return this.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
