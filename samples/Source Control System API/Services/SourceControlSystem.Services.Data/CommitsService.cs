namespace SourceControlSystem.Services.Data
{
    using System.Linq;
    using Models;
    using SourceControlSystem.Services.Data.Contracts;
    using SourceControlSystem.Data;

    public class CommitsService : ICommitsService
    {
        private readonly IRepository<Commit> commits;

        public CommitsService(IRepository<Commit> commitsRepository)
        {
            this.commits = commitsRepository;
        }

        public IQueryable<Commit> GetAllByProjectId(int id)
        {
            return this.commits
                .All()
                .Where(c => c.SoftwareProjectId == id);
        }

        public bool UserHasCommits(string username)
        {
            return this.commits
                .All()
                .Any(c => c.User.UserName == username);
        }
    }
}
