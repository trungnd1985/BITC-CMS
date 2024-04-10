namespace BITC.CMS.Identity.EntityFramework
{
    using BITC.CMS.Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;

    public class UserStore<TUser> : UserStore<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUserStore<TUser>, IUserStore<TUser, string>, IDisposable where TUser: IdentityUser
    {
        public UserStore() : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public UserStore(DbContext context) : base(context)
        {
        }
    }
}

