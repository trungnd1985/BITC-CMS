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
    public partial class PageMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<BITC.CMS.Data.Entity.Page>
    {
        public PageMap()
        {
            // table
            ToTable("Page", "dbo");

            // keys
            HasKey(t => t.PageID);

            // Properties
            Property(t => t.PageID)
                .HasColumnName("PageID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.PageTitle)
                .HasColumnName("PageTitle")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Description)
                .HasColumnName("Description")
                .IsOptional();
            Property(t => t.Keywords)
                .HasColumnName("Keywords")
                .IsOptional();
            Property(t => t.Body)
                .HasColumnName("Body")
                .IsOptional();
            Property(t => t.Template)
                .HasColumnName("Template")
                .HasMaxLength(255)
                .IsOptional();
            Property(t => t.Url)
                .HasColumnName("Url")
                .HasMaxLength(500)
                .IsRequired();
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
            Property(t => t.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasMaxLength(255)
                .IsOptional();
            Property(t => t.CreatedDate)
                .HasColumnName("CreatedDate")
                .IsOptional();
            Property(t => t.ModifiedBy)
                .HasColumnName("ModifiedBy")
                .HasMaxLength(50)
                .IsOptional();
            Property(t => t.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .IsOptional();
            Property(t => t.Expression).HasColumnName("Expression").IsOptional();
            Property(t => t.ParentID)
                .HasColumnName("ParentID")
                .IsOptional();
            Property(t => t.Path).HasColumnName("Path").HasColumnType("hierarchyid").IsOptional();

            // Relationships
            HasOptional(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.ParentID);

            InitializeMapping();
        }
    }
}

