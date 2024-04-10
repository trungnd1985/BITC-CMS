using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BITC.CMS.UI.Areas.Admin.Models
{
    public class LanguageModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}