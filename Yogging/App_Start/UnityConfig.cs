using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
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

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IContentRepository, ContentRepository>();
            container.RegisterType<IStoryService, StoryService>();
            container.RegisterType<ISprintService, SprintService>();
            container.RegisterType<ITagService, TagService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}