using System.Data.Entity;
using System.Diagnostics;
using DataAccess.Configuration;
using Domain;

namespace DataAccess.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BlogContext>());
        }

        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<Entry> Entries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BlogConfiguration());
            modelBuilder.Configurations.Add(new EntryConfiguration());
        }
    }
}
