namespace Books.Api.Models.ResponseModels
{
    using AutoMapper;
    using Books.Models;
    using Mappings;

    public class BookResponseModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Book, BookResponseModel>()
                .ForMember(b => b.Author, opt => opt.MapFrom(b => b.Author.FirstName + " " + b.Author.LastName));
        }
    }
}