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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITC.CMS.Data.Mapping
{
    public partial class ProjectCategoryMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<BITC.CMS.Data.Entity.ProjectCategory>
    {
        public ProjectCategoryMap()
        {
            // table
            ToTable("ProjectCategory", "dbo");

            // keys
            HasKey(t => t.ProjectCategoryID);

            // Properties
            Property(t => t.ProjectCategoryID)
                .HasColumnName("ProjectCategoryID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.ProjectCategoryName)
                .HasColumnName("ProjectCategoryName")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Description)
                .HasColumnName("Description")
                .HasMaxLength(1000)
                .IsOptional();
            Property(t => t.Inactive)
                .HasColumnName("Inactive")
                .IsRequired();
            Property(t => t.SortOrder)
                .HasColumnName("SortOrder")
                .IsRequired();
            Property(t => t.Culture)
                .HasColumnName("Culture")
                .HasMaxLength(10)
                .IsRequired();
            Property(t => t.CreatedDate)
                .HasColumnName("CreatedDate")
                .IsOptional();
            Property(t => t.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasMaxLength(255)
                .IsOptional();
            Property(t => t.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsOptional();
            Property(t => t.ModifiedBy)
                .HasColumnName("ModifiedBy")
                .HasMaxLength(255)
                .IsOptional();

            // Relationships

            InitializeMapping();
        }
    }
}
