using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Data.Mapping
{
    public class BlogEntryMap : EntityTypeConfiguration<BlogEntry>
    {
        public BlogEntryMap()
        {
            this.HasKey(t => t.BlogEntryID);

            this.Property(t => t.Title).IsRequired().HasMaxLength(255);
            this.Property(t => t.Body).IsRequired();
            this.Property(t => t.Culture).IsRequired().HasMaxLength(10);
            this.Property(t => t.AllowComment).IsRequired();
            this.Property(t => t.Inactive).IsRequired();

            this.ToTable("BlogEntry");

            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.AllowComment).HasColumnName("AllowComment");
            this.Property(t => t.BlogEntryID).HasColumnName("BlogEntryID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Culture).HasColumnName("Culture");
            this.Property(t => t.Inactive).HasColumnName("Inactive");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            //this.Property(t=>t.)
        }
    }
}
