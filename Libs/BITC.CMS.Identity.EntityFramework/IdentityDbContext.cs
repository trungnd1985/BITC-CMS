namespace BITC.CMS.Identity.EntityFramework
{
    using System;
    using System.Data.Common;
    using System.Data.Entity.Infrastructure;

    public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public IdentityDbContext() : this("DefaultConnection")
        {
        }

        public IdentityDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
        {
        }
    }
}

