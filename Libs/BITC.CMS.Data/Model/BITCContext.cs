using BITC.Library.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace BITC.CMS.Data.Entity
{
    public partial class BITCContext : DataContext
    {
        static BITCContext()
        {
            Database.SetInitializer<BITCContext>(null);
        }

        public BITCContext()
            : base("Name=BITCEntities")
        {
        }

        #region Property

        public DbSet<BlogEntry> BlogEntries { get; set; }

        public DbSet<BlogTag> BlogTags { get; set; }

        public DbSet<BlogEntryComment> BlogEntryComments { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Media> Media { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Module> Modules { get; set; }

        public System.Data.Entity.DbSet<BITC.CMS.Data.Entity.Menu> Menus { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectCategory> ProjectCategories { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.BlogEntryMap());
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.BlogEntryCommentMap());
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.PageMap());
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.SettingMap());
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.BlogTagMap());
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.ModuleMap());
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.MediaMap());
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.MenuMap());
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.ProjectCategoryMap());
            modelBuilder.Configurations.Add(new BITC.CMS.Data.Mapping.ProjectMap());
            //modelBuilder.Properties().Where(p => p.GetCustomAttributes(false).OfType<MaxLengthAttribute>().Any()).Configure(p => p.HasMaxLength(p.ClrPropertyInfo.GetCustomAttributes(false).OfType<MaxLengthAttribute>().FirstOrDefault().Length));
            //modelBuilder.Properties().Where(p => p.GetCustomAttributes(false).OfType<RequiredAttribute>().Any()).Configure(p => p.IsRequired());
            //modelBuilder.Properties().Where(p => p.GetCustomAttributes(false).OfType<KeyAttribute>().Any()).Configure(p => p.IsKey());
            //modelBuilder.Properties().Where(p => !p.GetType().IsGenericType).Configure(p => p.HasColumnName(p.ClrPropertyInfo.Name));
            //modelBuilder.Types().Configure(c => c.ToTable(c.ClrType.GetCustomAttributes(false).OfType<TableAttribute>().FirstOrDefault().Name));
        }

        private string GetTableName(Type type)
        {
            var pluralizationService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-US"));

            var result = pluralizationService.Pluralize(type.Name);

            result = Regex.Replace(result, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            return result.ToLower();
        }
    }
}
