using BITC.CMS.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Data.Model
{
    [MetadataType(typeof(PortfolioMetadata))]
    public partial class Portfolio
    {
        public class PortfolioMetadata
        {
            [Required]
            [MaxLength(255)]
            [UIHint("TextBox")]
            [Display(Name = "PortfolioName", ResourceType = typeof(PortfolioResource), Order = 0)]
            public string PortfolioName { get; set; }

            [Display(Name = "Description", ResourceType = typeof(CommonResource), Order = 1)]
            [UIHint("Textarea")]
            public string Description { get; set; }

            [Display(Name = "PortfolioImages", ResourceType = typeof(PortfolioResource), Order = 1)]
            [UIHint("_PortfolioImages")]
            public string PortfolioImages { get; set; }

            [Display(Name = "Year", ResourceType = typeof(PortfolioResource), Order = 3)]
            [UIHint("Integer")]
            public int Year { get; set; }

            [Display(Name = "Location", ResourceType = typeof(PortfolioResource), Order = 2)]
            [UIHint("TextBox")]
            public string Location { get; set; }

            [Display(Name = "IsFeatured", ResourceType = typeof(PortfolioResource), Order = 4)]
            [UIHint("Checkbox")]
            public bool IsFeatured { get; set; }

            [Display(Name = "SortOrder", ResourceType = typeof(CommonResource))]
            [DataType("Integer")]
            public int SortOrder { get; set; }

            [Display(Name = "Inactive", ResourceType = typeof(CommonResource))]
            [UIHint("Checkbox")]
            public bool Inactive { get; set; }
        }
    }
}
