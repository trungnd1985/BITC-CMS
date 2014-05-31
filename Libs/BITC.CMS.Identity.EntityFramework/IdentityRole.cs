namespace BITC.CMS.Identity.EntityFramework
{
    using System;

    public class IdentityRole : IdentityRole<string, IdentityUserRole>
    {
        public IdentityRole()
        {
            base.Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string roleName) : this()
        {
            base.Name = roleName;
        }
    }
}

