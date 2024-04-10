using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BITC.CMS.Data.Entity
{
    [MetadataType(typeof(NewsMetadata))]
    public partial class News : BaseEntity
    {
        public class NewsMetadata
        {

        }

        public List<int> NewsCategoriesID { get; set; }
    }
}