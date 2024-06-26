﻿namespace BITC.CMS.Identity.EntityFramework
{
    using BITC.CMS.Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class IdentityUser<TKey, TLogin, TRole, TClaim> : IUser<TKey>
        where TLogin : IdentityUserLogin<TKey>
        where TRole : IdentityUserRole<TKey>
        where TClaim : IdentityUserClaim<TKey>
    {
        public IdentityUser()
        {
            this.Claims = new List<TClaim>();
            this.Roles = new List<TRole>();
            this.Logins = new List<TLogin>();
        }

        public virtual int AccessFailedCount { get; set; }

        public virtual ICollection<TClaim> Claims { get; private set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual TKey Id { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual ICollection<TLogin> Logins { get; private set; }

        public virtual string PasswordHash { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual ICollection<TRole> Roles { get; private set; }

        public virtual string SecurityStamp { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual string UserName { get; set; }
    }
}

