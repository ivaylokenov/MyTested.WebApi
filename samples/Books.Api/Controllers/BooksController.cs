using System.Web.Http;

namespace Books.Api.Controllers
{
    using Books.Models;
    using Data;

    public class BooksController : ApiController
    {
        private readonly IRepository<Book> booksRepository;

        public BooksController(IRepository<Book> booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        public IHttpActionResult Get()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                
            }

            return Ok();
        }
    }
}
