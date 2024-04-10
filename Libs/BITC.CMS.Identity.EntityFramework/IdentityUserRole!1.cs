namespace BITC.CMS.Identity.EntityFramework
{
    using System;
    using System.Runtime.CompilerServices;

    public class IdentityUserRole<TKey>
    {
        public virtual TKey RoleId { get; set; }

        public virtual TKey UserId { get; set; }
    }
}

