namespace BITC.CMS.Identity.EntityFramework
{
    using BITC.CMS.Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;

    public class RoleStore<TRole> : RoleStore<TRole, string, IdentityUserRole>, IQueryableRoleStore<TRole>, IQueryableRoleStore<TRole, string>, IRoleStore<TRole, string>, IDisposable where TRole: IdentityRole, new()
    {
        public RoleStore() : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public RoleStore(DbContext context) : base(context)
        {
        }
    }
}

