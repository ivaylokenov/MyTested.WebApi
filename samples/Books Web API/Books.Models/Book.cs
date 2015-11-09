namespace Books.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
