﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;

namespace BITC.CMS.Data.Mapping
{
    public partial class BlogTagMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<BITC.CMS.Data.Entity.BlogTag>
    {
        public BlogTagMap()
        {
            // table
            ToTable("BlogTag", "dbo");

            // keys
            HasKey(t => t.BlogTagID);

            // Properties
            Property(t => t.BlogTagID)
                .HasColumnName("BlogTagID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.TagName)
                .HasColumnName("TagName")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Slug)
                .HasColumnName("Slug")
                .HasMaxLength(255)
                .IsRequired();

            // Relationships

            InitializeMapping();
        }
    }
}
