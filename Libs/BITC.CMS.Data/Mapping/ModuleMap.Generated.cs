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

namespace BITC.CMS.Data.Mapping
{
    public partial class ModuleMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<BITC.CMS.Data.Entity.Module>
    {
        public ModuleMap()
        {
            // table
            ToTable("Module", "dbo");

            // keys
            HasKey(t => t.ModuleID);

            // Properties
            Property(t => t.ModuleID)
                .HasColumnName("ModuleID")
                .IsRequired();
            Property(t => t.ModuleName)
                .HasColumnName("ModuleName")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Inactive)
                .HasColumnName("Inactive")
                .IsRequired();
            Property(t => t.IsSystem)
                .HasColumnName("IsSystem")
                .IsRequired();
            Property(t => t.CreatedDate)
                .HasColumnName("CreatedDate")
                .IsOptional();
            Property(t => t.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasMaxLength(255)
                .IsOptional();

            // Relationships

            InitializeMapping();
        }
    }
}

