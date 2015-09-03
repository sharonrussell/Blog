using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain;

namespace DataAccess.Configuration
{
    public class BlogConfiguration : EntityTypeConfiguration<Blog>
    {
        public BlogConfiguration()
        {
            Property(b => b.BlogId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasKey(b => b.BlogId);

            Property(b => b.Author)
                .IsRequired();
        }
    }
}