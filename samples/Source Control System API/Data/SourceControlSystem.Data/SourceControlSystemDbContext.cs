namespace SourceControlSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;

    public class SourceControlSystemDbContext : IdentityDbContext<User>, ISourceControlSystemDbContext
    {
        public SourceControlSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Commit> Commits { get; set; }

        public virtual IDbSet<SoftwareProject> SoftwareProjects { get; set; }

        public static SourceControlSystemDbContext Create()
        {
            return new SourceControlSystemDbContext();
        }
    }
}
