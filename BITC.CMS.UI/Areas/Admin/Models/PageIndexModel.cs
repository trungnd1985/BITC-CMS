using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BITC.CMS.UI.Areas.Admin.Models
{
    public class PageIndexModel
    {
        public int PageID { get; set; }

        public string PageTitle { get; set; }

        public string Url { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int SortOrder { get; set; }

        public string CreatedBy { get; set; }
    }
}