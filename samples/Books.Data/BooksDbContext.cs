namespace Books.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class BooksDbContext : IdentityDbContext<Author>, IBooksDbContext
    {
        public BooksDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Book> Books { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public static BooksDbContext Create()
        {
            return new BooksDbContext();
        }
    }
}
