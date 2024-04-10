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
    public partial class BlogEntryCommentMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<BITC.CMS.Data.Entity.BlogEntryComment>
    {
        public BlogEntryCommentMap()
        {
            // table
            ToTable("BlogEntryComment", "dbo");

            // keys
            HasKey(t => t.BlogEntryCommentID);

            // Properties
            Property(t => t.BlogEntryCommentID)
                .HasColumnName("BlogEntryCommentID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.BlogEntryID)
                .HasColumnName("BlogEntryID")
                .IsRequired();
            Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Email)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Message)
                .HasColumnName("Message")
                .IsRequired();
            Property(t => t.CreatedDate)
                .HasColumnName("CreatedDate")
                .IsRequired();
            Property(t => t.IsAuthor)
                .HasColumnName("IsAuthor")
                .IsRequired();

            // Relationships
            HasRequired(t => t.BlogEntry)
                .WithMany(t => t.BlogEntryComments)
                .HasForeignKey(d => d.BlogEntryID);

            InitializeMapping();
        }
    }
}

