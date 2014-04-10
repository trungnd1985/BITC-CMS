using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.Library.Data
{
    public interface IEntity
    {
        string Description { get; set; }

        int SortOrder { get; set; }

        bool Inactive { get; set; }

        string CreatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        string ModifiedBy { get; set; }

        DateTime ModifiedDate { get; set; }

        string Culture { get; set; }
    }
}
