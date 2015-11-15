namespace SourceControlSystem.Api.Models.Projects
{
    using Common.Constants;
    using System.ComponentModel.DataAnnotations;

    public class SaveProjectRequestModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaxProjectName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaxProjectDescription)]
        public string Description { get; set; }

        public bool Private { get; set; }
    }
}
