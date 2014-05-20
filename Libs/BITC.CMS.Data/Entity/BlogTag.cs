using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Data.Entity
{
    public partial class BlogTag : BaseEntity
    {
        public BlogTag()
        {
            this.BlogEntries = new HashSet<BlogEntry>();
        }

        public int BlogTagID { get; set; }
        public string TagName { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<BlogEntry> BlogEntries { get; set; }
    }
}
