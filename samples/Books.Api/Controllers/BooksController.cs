using System.Web.Http;

namespace Books.Api.Controllers
{
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Books.Models;
    using Data;
    using Models.ResponseModels;

    public class BooksController : ApiController
    {
        private readonly IRepository<Book> booksRepository;

        public BooksController(IRepository<Book> booksRepository)
        {
            this.booksRepository = booksRepository;
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
                    .Project()
                    .To<BookResponseModel>()
                    .ToList());
            }

            return this.Ok(this.booksRepository
                    .All()
                    .OrderByDescending(b => b.Id)
                    .Take(3)
                    .Project()
                    .To<BookResponseModel>()
                    .ToList());
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            if (this.User.Identity.IsAuthenticated) // TODO: remove when Authorize attribute is validated in MyWebApi
            {
                return this.Ok(this.booksRepository
                    .All()
                    .Where(b => b.Id == id)
                    .Project()
                    .To<BookResponseModel>()
                    .FirstOrDefault());
            }

            return this.Unauthorized();
        }
    }
}
