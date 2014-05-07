using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations
{
    public class TemplateRenderAttribute : Attribute
    {
        public bool Display { get; set; }

        public int Order { get; set; }


    }
}
