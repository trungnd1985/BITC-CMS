using BITC.CMS.Resource;
using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BITC.CMS.Data.Entity
{
    [Table("Media")]
    public partial class Media : BaseEntity
    {
        [Key]
        public int MediaID { get; set; }

        public int? MediaType { get; set; }

        public string MIMEType { get; set; }

        public string Caption { get; set; }        

        public string AlternativeText { get; set; }

        public string Description { get; set; }

        public string Dimension { get; set; }

        public long FileSize { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }

        public System.DateTime? UploadedDate { get; set; }

        public string Author { get; set; }

        public string Url { get; set; }
    }
}
