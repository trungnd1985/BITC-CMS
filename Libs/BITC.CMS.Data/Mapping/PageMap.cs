using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Data.Mapping
{
    public class PageMap : EntityTypeConfiguration<Page>
    {
        public PageMap()
        {
            this.HasKey(t => t.PageID);

            this.Property(t => t.PageTitle).IsRequired().HasMaxLength(255);
            this.Property(t => t.Culture).IsRequired().HasMaxLength(10);
            this.Property(t => t.Url).IsRequired().HasMaxLength(500);
            this.Property(t => t.SortOrder).IsRequired();
            this.Property(t => t.Inactive).IsRequired();

            this.ToTable("Page");
        }
    }
}
