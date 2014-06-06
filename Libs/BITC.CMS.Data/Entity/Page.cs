using BITC.CMS.Resource;
using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Hierarchy;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BITC.CMS.Data.Entity
{
    [Table("Page")]
    public partial class Page : BaseEntity
    {
        [Key]
        public int PageID { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "PageTitle", ResourceType = typeof(PageResource), Order = 0)]
        [UIHint("TextBox")]
        public string PageTitle { get; set; }

        [Display(Name = "Description", ResourceType = typeof(CommonResource), Order = 5)]
        [UIHint("Textarea")]
        public string Description { get; set; }

        [Display(Name = "Keywords", ResourceType = typeof(PageResource), Order = 4)]
        [UIHint("Textarea")]
        public string Keywords { get; set; }

        [AllowHtml()]
        [Display(Name = "Body", ResourceType = typeof(PageResource), Order = 3)]
        [UIHint("Html")]
        public string Body { get; set; }

        [Display(Name = "Template", ResourceType = typeof(PageResource), Order = 2)]
        [UIHint("TemplateDropDownList")]
        public string Template { get; set; }

        [MaxLength(500)]
        [UIHint("TextBox")]
        [Display(Name = "Url", ResourceType = typeof(PageResource), Order = 1)]
        [AdditionalMetadata("Prefix", "http://domain.com/")]
        public string Url { get; set; }

        [Display(Name = "Inactive", ResourceType = typeof(CommonResource))]
        [UIHint("Checkbox")]
        public bool Inactive { get; set; }

        [Display(Name = "SortOrder", ResourceType = typeof(CommonResource))]
        [DataType("Integer")]
        public int SortOrder { get; set; }

        [Display(Name = "Parent", ResourceType = typeof(CommonResource))]
        [DataType("PageParent")]
        public int? ParentID { get; set; }

        [Display(Name = "Expression", ResourceType = typeof(PageResource))]
        public string Expression { get; set; }

        public HierarchyId Path { get; set; }

        public string Culture { get; set; }

        public string CreatedBy { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual Page Parent { get; set; }
        public virtual ICollection<Page> Children { get; set; }
    }
}
