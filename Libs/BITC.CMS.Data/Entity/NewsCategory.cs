using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BITC.CMS.Data.Entity
{
    [MetadataType(typeof(NewsCategoryMetadata))]
    public partial class NewsCategory : BaseEntity
    {
        public class NewsCategoryMetadata
        {

        }
    }
}