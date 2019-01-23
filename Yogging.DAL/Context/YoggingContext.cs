using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Yogging.DAL.Profiles;
using Yogging.DAL.Sprints;
using Yogging.DAL.Stories;
using Yogging.DAL.Tags;
using Yogging.DAL.Users;
using Yogging.Domain.Users;

namespace Yogging.DAL.Context
{
    public class YoggingContext : DbContext
    {
        public YoggingContext() : base("YoggingContext")
        {
        }

        public DbSet<StoryDao> Stories { get; set; }
        public DbSet<UserDao> Users { get; set; }
        public DbSet<SprintDao> Sprints { get; set; }
        public DbSet<TagDao> Tags { get; set; }
        public DbSet<ProfileDao> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
