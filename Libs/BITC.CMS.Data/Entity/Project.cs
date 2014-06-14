using BITC.CMS.Resource;
using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BITC.CMS.Data.Entity
{
    [MetadataType(typeof(ProjectMetadata))]
    public partial class Project : BaseEntity
    {
        public class ProjectMetadata
        {
            [Required]
            [MaxLength(255)]
            [Display(Name = "ProjectName", ResourceType = typeof(ProjectResource), Order = 0)]
            [UIHint("TextBox")]
            public string ProjectName { get; set; }

            [Display(Name = "Description", ResourceType = typeof(CommonResource), Order = 1)]
            [UIHint("Textarea")]
            public string Description { get; set; }

            [Display(Name = "SortOrder", ResourceType = typeof(CommonResource), Order = 3)]
            [DataType("Integer")]
            public int SortOrder { get; set; }

            [Required]
            [Display(Name = "Inactive", ResourceType = typeof(CommonResource), Order = 2)]
            [UIHint("Checkbox")]
            public bool Inactive { get; set; }

            [Required]
            [Display(Name = "IsFeatured", ResourceType = typeof(ProjectResource), Order = 2)]
            [UIHint("Checkbox")]
            public bool IsFeatured { get; set; }

            [MaxLength(255)]
            [Display(Name = "Location", ResourceType = typeof(ProjectResource), Order = 0)]
            [UIHint("TextBox")]
            public string Location { get; set; }

            [Display(Name = "Year", ResourceType = typeof(ProjectResource), Order = 3)]
            [DataType("Integer")]
            public int? Year { get; set; }
        }
    }
}