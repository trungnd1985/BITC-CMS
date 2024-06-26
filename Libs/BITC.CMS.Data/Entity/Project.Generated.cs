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
    public partial class Project
    {
        public Project()
        {
            ProjectCategories = new List<ProjectCategory>();
            ProjectCategoriesID = new List<int>();
        }

        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Culture { get; set; }
        public int SortOrder { get; set; }
        public bool Inactive { get; set; }
        public bool IsFeatured { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Location { get; set; }
        public int? Year { get; set; }
        public string ProjectImages { get; set; }

        public int? ClientID { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<ProjectCategory> ProjectCategories { get; set; }
    }
}