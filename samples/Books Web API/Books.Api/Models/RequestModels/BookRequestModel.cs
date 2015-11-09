namespace Books.Api.Models.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class BookRequestModel
    {
        [Required]
        [MinLength(3)]
        public string AuthorUsername { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}