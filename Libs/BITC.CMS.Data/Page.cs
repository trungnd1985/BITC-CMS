//using BITC.CMS.Resource;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

//namespace BITC.CMS.Data.Entity
//{
//    [MetadataType(typeof(PageMetadata))]
//    public partial class Page
//    {
//        public class PageMetadata
//        {
//            [Required]
//            [MaxLength(255)]
//            [Display(Name = "PageTitle", ResourceType = typeof(PageResource), Order = 0)]
//            [UIHint("TextBox")]
//            public string PageTitle { get; set; }

//            [AllowHtml()]
//            [Display(Name = "Body", ResourceType = typeof(PageResource), Order = 3)]
//            [UIHint("Html")]
//            public string Body { get; set; }

//            [MaxLength(500)]
//            [UIHint("TextBox")]
//            [Display(Name = "Url", ResourceType = typeof(PageResource), Order = 1)]        
//            [AdditionalMetadata("Prefix","http://domain.com/")]
//            public string Url { get; set; }

//            [Display(Name = "Template", ResourceType = typeof(PageResource), Order = 2)]
//            [UIHint("TemplateDropDownList")]
//            public string Template { get; set; }

//            [Display(Name = "Keywords", ResourceType = typeof(PageResource), Order = 4)]
//            [UIHint("Textarea")]
//            public string Keywords { get; set; }

//            [Display(Name = "Description", ResourceType = typeof(CommonResource), Order = 5)]
//            [UIHint("Textarea")]
//            public string Description { get; set; }

//            [DefaultValue("vi-VN")]
//            [HiddenInput(DisplayValue = false)]
//            public string Culture { get; set; }

//            [Display(Name = "SortOrder", ResourceType = typeof(CommonResource))]
//            [DataType("Integer")]
//            public int SortOrder { get; set; }

//            [Display(Name = "Inactive", ResourceType = typeof(CommonResource))]
//            [UIHint("Checkbox")]
//            public bool Inactive { get; set; }
//        }
//    }
//}
