using AutoMapper;
using Microsoft.Owin;
using Owin;
using Yogging.Domain.Sprints;
using Yogging.Domain.Stories;
using Yogging.Domain.Tags;
using Yogging.Domain.Users;
using Yogging.ViewModels;

[assembly: OwinStartupAttribute(typeof(Yogging.Startup))]
namespace Yogging
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Sprint, SprintViewModel>();
                config.CreateMap<Story, StoryViewModel>();
                config.CreateMap<User, UserViewModel>();
                config.CreateMap<Tag, TagViewModel>();
            });
        }
    }
}
