namespace SourceControlSystem.Api.Models.Commits
{
    using Infrastructure.Mapping;
    using SourceControlSystem.Models;
    using System;
    using AutoMapper;

    public class ListedCommitResponseModel : IMapFrom<Commit>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Commit, ListedCommitResponseModel>()
                .ForMember(c => c.UserName, opt => opt.MapFrom(c => c.User.UserName));
        }
    }
}
