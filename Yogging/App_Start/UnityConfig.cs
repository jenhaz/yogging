using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using Yogging.Controllers;
using Yogging.DAL.Repository;
using Yogging.Services.Implementations;
using Yogging.Services.Interfaces;

namespace Yogging
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterType<IContentRepository, ContentRepository>();
            container.RegisterType<IStoryService, StoryService>();
            container.RegisterType<ISprintService, SprintService>();
            container.RegisterType<ITagService, TagService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IBlogService, BlogService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}