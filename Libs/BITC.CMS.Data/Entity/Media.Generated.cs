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
    public partial class Media
    {
        public Media()
        {
        }

        public int MediaID { get; set; }
        public int? MediaType { get; set; }
        public string MIMEType { get; set; }
        public string Caption { get; set; }
        public string AlternativeText { get; set; }
        public string Description { get; set; }
        public string Dimension { get; set; }
        public long FileSize { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public System.DateTime? UploadedDate { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }

    }
}