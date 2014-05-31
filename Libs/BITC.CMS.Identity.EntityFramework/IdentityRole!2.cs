namespace BITC.CMS.Identity.EntityFramework
{
    using BITC.CMS.Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class IdentityRole<TKey, TUserRole> : IRole<TKey> where TUserRole: IdentityUserRole<TKey>
    {
        public IdentityRole()
        {
            this.Users = new List<TUserRole>();
        }

        public TKey Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<TUserRole> Users { get; private set; }
    }
}

