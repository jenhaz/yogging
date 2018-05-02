using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Yogging.Models;

namespace Yogging.DAL.Context
{
    public class YoggingContext : DbContext
    {
        public YoggingContext() : base("YoggingContext")
        {
        }

        public DbSet<Story> Stories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
