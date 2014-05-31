namespace BITC.CMS.Identity.EntityFramework
{
    using System;
    using System.Runtime.CompilerServices;

    public class IdentityUserLogin<TKey>
    {
        public virtual string LoginProvider { get; set; }

        public virtual string ProviderKey { get; set; }

        public virtual TKey UserId { get; set; }
    }
}

