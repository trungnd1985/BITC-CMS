using BITC.CMS.Resource;
using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BITC.CMS.Data.Entity
{
    [Table("BlogEntry")]
    public partial class BlogEntry : BaseEntity
    {
        public BlogEntry()
        {
            this.BlogTags = new HashSet<BlogTag>();
        }

        [Key]
        public int BlogEntryID { get; set; }

        List<int> _selectedTags;

        [Display(Name = "BlogTags", ResourceType = typeof(BlogResource), Order = 2)]
        [UIHint("BlogTagMultiSelect")]
        [NotMapped()]
        public List<int> SelectedTags
        {
            get;
            set;


        }

        [Required]
        [MaxLength(255)]
        [Display(Name = "BlogTitle", ResourceType = typeof(BlogResource), Order = 0)]
        [UIHint("TextBox")]
        public string BlogTitle { get; set; }

        [Required]
        [Display(Name = "Body", ResourceType = typeof(BlogResource), Order = 1)]
        [AllowHtml]
        [UIHint("Html")]
        public string Body { get; set; }

        [MaxLength(255)]
        [Display(Name = "Source", ResourceType = typeof(BlogResource), Order = 3)]
        [UIHint("TextBox")]
        public string Source { get; set; }

        [DefaultValue("vi-VN")]
        public string Culture { get; set; }

        [Display(Name = "AllowComment", ResourceType = typeof(BlogResource), Order = 5)]
        [UIHint("Checkbox")]
        public bool AllowComment { get; set; }

        [Required]
        [Display(Name = "Inactive", ResourceType = typeof(CommonResource), Order = 4)]
        [UIHint("Checkbox")]
        public bool Inactive { get; set; }

        [Display(Name = "SortOrder", ResourceType = typeof(CommonResource))]
        [DataType("Integer")]
        public Nullable<int> SortOrder { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }


        public virtual ICollection<BlogTag> BlogTags { get; set; }
    }
}
