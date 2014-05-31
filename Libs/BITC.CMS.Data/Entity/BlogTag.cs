using BITC.CMS.Resource;
using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Data.Entity
{
    [Table("BlogTag")]
    public partial class BlogTag : BaseEntity
    {
        public BlogTag()
        {
            this.BlogEntries = new HashSet<BlogEntry>();
        }

        [Key]
        public int BlogTagID { get; set; }
        public string Culture { get; set; }

        [Display(Name = "TagName", ResourceType = typeof(BlogResource))]
        public string TagName { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<BlogEntry> BlogEntries { get; set; }
    }
}
