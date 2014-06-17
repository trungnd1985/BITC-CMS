using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BITC.CMS.Data.Entity
{
    [MetadataType(typeof(MenuMetadata))]
    public partial class Menu : BaseEntity
    {
        public class MenuMetadata
        {

        }
    }
}