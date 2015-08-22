namespace MyWebApi.Tests.ControllerSetups
{
    using System.ComponentModel.DataAnnotations;

    public class RequestModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
