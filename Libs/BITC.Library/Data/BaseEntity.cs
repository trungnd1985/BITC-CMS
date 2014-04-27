using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.Library.Data
{
    public class BaseEntity
    {
        public string Description { get; set; }

        public int SortOrder { get; set; }

        public bool Inactive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Culture { get; set; }
    }
}
