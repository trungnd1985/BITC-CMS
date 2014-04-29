using BITC.CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BITC.CMS.UI.Areas.Admin.Models
{
    public class PageDetailModel
    {
        public string PageTitle { get; set; }

        public string Url { get; set; }

        public string Body { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }
        
        public string Template { get; set; }

        public bool Inactive { get; set; }

        public int SortOrder { get; set; }
    }
}