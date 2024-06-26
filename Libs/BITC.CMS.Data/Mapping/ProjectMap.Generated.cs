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
    public partial class ProjectMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<BITC.CMS.Data.Entity.Project>
    {
        public ProjectMap()
        {
            // table
            ToTable("Project", "dbo");

            // keys
            HasKey(t => t.ProjectID);

            // Properties
            Property(t => t.ProjectID)
                .HasColumnName("ProjectID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.ProjectName)
                .HasColumnName("ProjectName")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Description)
                .HasColumnName("Description")
                .IsOptional();
            Property(t => t.Culture)
                .HasColumnName("Culture")
                .HasMaxLength(10)
                .IsRequired();
            Property(t => t.SortOrder)
                .HasColumnName("SortOrder")
                .IsRequired();
            Property(t => t.Inactive)
                .HasColumnName("Inactive")
                .IsRequired();
            Property(t => t.IsFeatured)
                .HasColumnName("IsFeatured")
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
            Property(t => t.Location)
                .HasColumnName("Location")
                .HasMaxLength(255)
                .IsOptional();
            Property(t => t.Year)
                .HasColumnName("Year")
                .IsOptional();
            Property(t => t.ProjectImages)
                .HasColumnName("ProjectImages")
                .IsOptional();
            Property(t => t.ClientID)
                .HasColumnName("ClientID")
                .IsOptional();

            // Relationships
            HasOptional(t => t.Client)
                .WithMany(t => t.Projects)
                .HasForeignKey(d => d.ClientID);
            HasMany(t => t.ProjectCategories)
                .WithMany(t => t.Projects)
                .Map(m =>
                {
                    m.ToTable("ProjectsInCategory", "dbo");
                    m.MapLeftKey("ProjectID");
                    m.MapRightKey("ProjectCategoryID");
                });

            InitializeMapping();
        }
    }
}

