namespace Books.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Books.Models;
    using Data;
    using Models.RequestModels;
    using Models.ResponseModels;

    public class BooksController : ApiController
    {
        private readonly IRepository<Book> booksRepository;
        private readonly IRepository<Author> authorsRepository; 

        public BooksController(IRepository<Book> booksRepository, IRepository<Author> authorsRepository)
        {
            this.booksRepository = booksRepository;
            this.authorsRepository = authorsRepository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Ok(this.booksRepository
                    .All()
                    .OrderByDescending(b => b.Id)
                    .Take(10)
                    .ProjectTo<BookResponseModel>()
                    .ToList());
            }

            return this.Ok(this.booksRepository
                    .All()
                    .OrderByDescending(b => b.Id)
                    .Take(3)
                    .ProjectTo<BookResponseModel>()
                    .ToList());
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post(BookRequestModel book)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var author = this.authorsRepository
                .All()
                .FirstOrDefault(a => a.UserName == book.AuthorUsername);

            if (author == null)
            {
                return this.BadRequest("Author does not exist!");
            }

            var newBook = new Book
            {
                Title = book.Title,
                Description = book.Description,
                Author = author
            };

            this.booksRepository.Add(newBook);
            this.booksRepository.SaveChanges();

            return this.Created(string.Format("/Books/GetById/{0}", newBook.Id), Mapper.Map<BookResponseModel>(newBook));
        }

        [HttpGet]
        [Authorize]
        [Route("Books/GetById/{id}")]
        public IHttpActionResult Get(int id)
        {
            return this.Ok(this.booksRepository
                .All()
                .Where(b => b.Id == id)
                .ProjectTo<BookResponseModel>()
                .FirstOrDefault());
        }
    }
}
