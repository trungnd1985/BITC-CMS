﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace BITC.CMS.Data.Entity
{
    public partial class BlogEntry
    {
        public BlogEntry()
        {
            BlogTags = new List<BlogTag>();
            BlogEntryComments = new List<BlogEntryComment>();
        }

        public int BlogEntryID { get; set; }
        public string BlogTitle { get; set; }
        public string Body { get; set; }
        public string Culture { get; set; }
        public bool AllowComment { get; set; }
        public bool Inactive { get; set; }
        public int? SortOrder { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Source { get; set; }
        public System.DateTime? PublishDate { get; set; }

        public virtual ICollection<BlogTag> BlogTags { get; set; }
        public virtual ICollection<BlogEntryComment> BlogEntryComments { get; set; }
    }
}