namespace BITC.CMS.Identity.EntityFramework
{
    using BITC.CMS.Identity;
    using Microsoft.AspNet.Identity;
    using System;

    public class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser, IUser<string>
    {
        public IdentityUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public IdentityUser(string userName) : this()
        {
            this.UserName = userName;
        }
    }
}

