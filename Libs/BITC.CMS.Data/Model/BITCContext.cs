using BITC.CMS.Data.Entity;
using BITC.CMS.Data.Mapping;
using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;

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

        //public DbSet<BlogEntry> BlogEntries { get; set; }

        //public DbSet<BlogTag> BlogTags { get; set; }

        public DbSet<Page> Pages { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new BlogTagMap());
            //modelBuilder.Configurations.Add(new BlogEntryMap());B
            //modelBuilder.Configurations.Add(new PageMap());

            modelBuilder.Properties().Where(p => p.GetCustomAttributes(false).OfType<MaxLengthAttribute>().Any()).Configure(p => p.HasMaxLength(p.ClrPropertyInfo.GetCustomAttributes(false).OfType<MaxLengthAttribute>().FirstOrDefault().Length));
            modelBuilder.Properties().Where(p => p.GetCustomAttributes(false).OfType<RequiredAttribute>().Any()).Configure(p => p.IsRequired());
            modelBuilder.Properties().Where(p => p.GetCustomAttributes(false).OfType<KeyAttribute>().Any()).Configure(p => p.IsKey());
            modelBuilder.Properties().Where(p => !p.GetType().IsGenericType).Configure(p => p.HasColumnName(p.ClrPropertyInfo.Name));
            modelBuilder.Types().Configure(c => c.ToTable(c.ClrType.GetCustomAttributes(false).OfType<TableAttribute>().FirstOrDefault().Name));
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
