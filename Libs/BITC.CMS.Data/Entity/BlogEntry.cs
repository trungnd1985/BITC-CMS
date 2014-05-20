using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Data.Entity
{
    public partial class BlogEntry : BaseEntity
    {
        public BlogEntry()
        {
            this.BlogTags = new HashSet<BlogTag>();
        }

        public int BlogEntryID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Culture { get; set; }
        public bool AllowComment { get; set; }
        public bool Inactive { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<BlogTag> BlogTags { get; set; }
    }
}
