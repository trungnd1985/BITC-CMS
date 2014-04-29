using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Data.Model
{
    [MetadataType(typeof(PageMetaData))]
    public partial class Page
    {
        public class PageMetaData
        {
            [Required]
            [MaxLength(255)]
            public string PageTitle { get; set; }

            [MaxLength(500)]
            public string Url { get; set; }

            [DefaultValue("vi-VN")]
            public string Culture { get; set; }
        }
    }
}
