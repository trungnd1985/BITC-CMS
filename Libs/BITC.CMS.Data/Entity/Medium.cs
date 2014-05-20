using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Data.Entity
{
    public partial class Medium : BaseEntity
    {
        public int MediaID { get; set; }

        public int MediaType { get; set; }

        public string MIMEType { get; set; }

        public int MyProperty { get; set; }
    }
}
