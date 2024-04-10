using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.Library.Data
{
    public abstract class BaseEntity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
