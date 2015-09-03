using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain;

namespace DataAccess.Configuration
{
    public class EntryConfiguration : EntityTypeConfiguration<Entry>
    {
        public EntryConfiguration()
        {
            HasKey(e => e.EntryId);

            Property(e => e.EntryId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(e => e.Blog)
                .WithMany(b => b.Entries)
                .HasForeignKey(b => b.BlogId)
                .WillCascadeOnDelete();

            Property(e => e.Title)
                .IsRequired();
            
            Property(e => e.Body)
                .IsRequired();

            Property(e => e.Date)
                .IsRequired();
        }
    }
}