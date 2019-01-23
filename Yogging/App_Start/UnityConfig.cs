using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using Yogging.Blogs;
using Yogging.Controllers;
using Yogging.DAL.Profiles;
using Yogging.DAL.Sprints;
using Yogging.DAL.Stories;
using Yogging.DAL.Tags;
using Yogging.DAL.Users;
using Yogging.Domain.Profiles;
using Yogging.Domain.Sprints;
using Yogging.Domain.Stories;
using Yogging.Domain.Tags;
using Yogging.Domain.Users;
using Yogging.Spotify;
using Yogging.Sprints;
using Yogging.Stories;
using Yogging.Tags;
using Yogging.Users;

namespace Yogging
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterType<IBlogService, BlogService>();
            container.RegisterType<ISpotifyService, SpotifyService>();
            container.RegisterType<ISprintService, SprintService>();
            container.RegisterType<IStoryService, StoryService>();
            container.RegisterType<ITagService, TagService>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IProfileRepository, ProfileRepository>();
            container.RegisterType<ISprintRepository, SprintRepository>();
            container.RegisterType<IStoryRepository, StoryRepository>();
            container.RegisterType<ITagRepository, TagRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}