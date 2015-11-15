namespace SourceControlSystem.Services.Data.Contracts
{
    using SourceControlSystem.Models;
    using System.Linq;

    public interface ICommitsService
    {
        IQueryable<Commit> GetAllByProjectId(int id);

        bool UserHasCommits(string username);
    }
}
