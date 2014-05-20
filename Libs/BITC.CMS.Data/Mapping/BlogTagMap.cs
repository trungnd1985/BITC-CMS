using BITC.CMS.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace BITC.CMS.Data.Mapping
{
    public class BlogTagMap : EntityTypeConfiguration<BlogTag>
    {
        public BlogTagMap()
        {
            // Primary Key
            this.HasKey(t => t.BlogTagID);

            // Properties
            this.Property(t => t.TagName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("BlogTag");
            this.Property(t => t.BlogTagID).HasColumnName("BlogTagID");
            this.Property(t => t.TagName).HasColumnName("TagName");
            this.Property(t => t.Slug).HasColumnName("Slug");

            //this.HasOptional(t => t.BlogEntries)
            //    .WithMany(t=>t.)
            //    .HasForeignKey(d => d.BlogTagID);
        }
    }
}
