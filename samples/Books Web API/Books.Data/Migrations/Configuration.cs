namespace Books.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<BooksDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BooksDbContext context)
        {
            var author = new Author { UserName = "TestUser", FirstName = "Test", LastName = "User" };

            context.Users.AddOrUpdate(a => a.UserName, author);

            context.Books.AddOrUpdate(b => b.Title,
                new Book { Title = "First book", Description = "First book description", Author = author},
                new Book { Title = "Second book", Description = "Second book description", Author = author },
                new Book { Title = "Third book", Description = "Third book description", Author = author },
                new Book { Title = "Fourth book", Description = "Fourth book description", Author = author },
                new Book { Title = "Fifth book", Description = "Fifth book description", Author = author },
                new Book { Title = "Sixth book", Description = "Sixth book description", Author = author },
                new Book { Title = "Seventh book", Description = "Seventh book description", Author = author },
                new Book { Title = "Eighth book", Description = "Eighth book description", Author = author },
                new Book { Title = "Ninth book", Description = "Ninth book description", Author = author },
                new Book { Title = "Tenth book", Description = "Tenth book description", Author = author });
        }
    }
}
