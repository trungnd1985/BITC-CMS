using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BITC.Web.Library
{

    public class PageInfo : BaseEntity
    {
        public int PageID { get; set; }

        [Required]
        [MaxLength(255)]
        public string PageTitle { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        public string Template { get; set; }

        [Required]
        [MaxLength(500)]
        public string Url { get; set; }
    }
}
