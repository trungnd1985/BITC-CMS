using BITC.CMS.Resource;
using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BITC.CMS.Data.Entity
{
    [MetadataType(typeof(ProjectCategoryMetadata))]
    public partial class ProjectCategory : BaseEntity
    {
        public class ProjectCategoryMetadata
        {
            [Required]
            [MaxLength(255)]
            [Display(Name = "ProjectCategoryName", ResourceType = typeof(ProjectResource), Order = 0)]
            [UIHint("TextBox")]
            public string ProjectCategoryName { get; set; }

            [Display(Name = "Description", ResourceType = typeof(CommonResource), Order = 1)]
            [UIHint("Textarea")]
            public string Description { get; set; }

            [Required]
            [Display(Name = "Inactive", ResourceType = typeof(CommonResource), Order = 2)]
            [UIHint("Checkbox")]
            public string Inactive { get; set; }

            [Display(Name = "SortOrder", ResourceType = typeof(CommonResource), Order = 3)]
            [DataType("Integer")]
            public int SortOrder { get; set; }
        }
    }
}