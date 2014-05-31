namespace BITC.CMS.Identity.EntityFramework
{
    using BITC.CMS.Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : IUserLoginStore<TUser, TKey>, IUserClaimStore<TUser, TKey>, IUserRoleStore<TUser, TKey>, IUserPasswordStore<TUser, TKey>, IUserSecurityStampStore<TUser, TKey>, IQueryableUserStore<TUser, TKey>, IUserEmailStore<TUser, TKey>, IUserPhoneNumberStore<TUser, TKey>, IUserTwoFactorStore<TUser, TKey>, IUserLockoutStore<TUser, TKey>, IUserStore<TUser, TKey>, IDisposable where TUser: IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim> where TRole: IdentityRole<TKey, TUserRole> where TKey: IEquatable<TKey> where TUserLogin: IdentityUserLogin<TKey>, new() where TUserRole: IdentityUserRole<TKey>, new() where TUserClaim: IdentityUserClaim<TKey>, new()
    {
        private bool _disposed;
        private readonly IDbSet<TUserLogin> _logins;
        private readonly EntityStore<TRole> _roleStore;
        private readonly IDbSet<TUserClaim> _userClaims;
        private EntityStore<TUser> _userStore;

        public UserStore(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.Context = context;
            this.AutoSaveChanges = true;
            this._userStore = new EntityStore<TUser>(context);
            this._roleStore = new EntityStore<TRole>(context);
            this._logins = this.Context.Set<TUserLogin>();
            this._userClaims = this.Context.Set<TUserClaim>();
        }

        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            TUserClaim item = Activator.CreateInstance<TUserClaim>();
            item.UserId = user.Id;
            item.ClaimType = claim.Type;
            item.ClaimValue = claim.Value;
            user.Claims.Add(item);
            return Task.FromResult<int>(0);
        }

        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            TUserLogin item = Activator.CreateInstance<TUserLogin>();
            item.UserId = user.Id;
            item.ProviderKey = login.ProviderKey;
            item.LoginProvider = login.LoginProvider;
            user.Logins.Add(item);
            return Task.FromResult<int>(0);
        }

        public virtual Task AddToRoleAsync(TUser user, string roleName)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }
            TRole local = this._roleStore.DbEntitySet.SingleOrDefault<TRole>(r => r.Name.ToUpper() == roleName.ToUpper());
            if (local == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, IdentityResources.RoleNotFound, new object[] { roleName }));
            }
            TUserRole local3 = Activator.CreateInstance<TUserRole>();
            local3.UserId = user.Id;
            local3.RoleId = local.Id;
            TUserRole item = local3;
            user.Roles.Add(item);
            local.Users.Add(item);
            return Task.FromResult<int>(0);
        }

        public async virtual Task CreateAsync(TUser user)
        {
            ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this)._userStore.Create(user);
            await ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).SaveChanges();
        }

        public async virtual Task DeleteAsync(TUser user)
        {
            ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this)._userStore.Delete(user);
            await ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if ((this.DisposeContext && disposing) && (this.Context != null))
            {
                this.Context.Dispose();
            }
            this._disposed = true;
            this.Context = null;
            this._userStore = null;
        }

        public async virtual Task<TUser> FindAsync(UserLoginInfo login)
        {
            TUserLogin userLogin;
            ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).ThrowIfDisposed();
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            string provider = login.LoginProvider;
            string key = login.ProviderKey;
            TUserLogin introduced16 = await ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this)._logins.FirstOrDefaultAsync<TUserLogin>((Expression<Func<TUserLogin, bool>>) (l => ((l.LoginProvider == provider) && (l.ProviderKey == key))));            
            userLogin = introduced16;
            if (userLogin != null)
            {
                return await ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).GetUserAggregateAsync(u => u.Id.Equals(userLogin.UserId));
            }
            return default(TUser);
        }

        public Task<TUser> FindByEmailAsync(string email)
        {
            this.ThrowIfDisposed();
            return this.GetUserAggregateAsync(u => u.Email.ToUpper() == email.ToUpper());
        }

        public virtual Task<TUser> FindByIdAsync(TKey userId)
        {
            this.ThrowIfDisposed();
            return this.GetUserAggregateAsync(u => u.Id.Equals(userId));
        }

        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            this.ThrowIfDisposed();
            return this.GetUserAggregateAsync(u => u.UserName.ToUpper() == userName.ToUpper());
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<int>(user.AccessFailedCount);
        }

        public virtual Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<IList<Claim>>((from c in user.Claims select new Claim(c.ClaimType, c.ClaimValue)).ToList<Claim>());
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.EmailConfirmed);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<DateTimeOffset>(user.LockoutEndDateUtc.HasValue ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) : new DateTimeOffset());
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<IList<UserLoginInfo>>((from l in user.Logins select new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList<UserLoginInfo>());
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.PhoneNumberConfirmed);
        }

        public virtual Task<IList<string>> GetRolesAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<IList<string>>((from userRoles in user.Roles
                join roles in this._roleStore.DbEntitySet on userRoles.RoleId equals roles.Id
                select roles.Name).ToList<string>());
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.SecurityStamp);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.TwoFactorEnabled);
        }

        private Task<TUser> GetUserAggregateAsync(Expression<Func<TUser, bool>> filter)
        {
            return this.Users.Include<TUser, ICollection<TUserRole>>(u => u.Roles).Include<TUser, ICollection<TUserClaim>>(u => u.Claims).Include<TUser, ICollection<TUserLogin>>(u => u.Logins).FirstOrDefaultAsync<TUser>(filter);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult<bool>(user.PasswordHash != null);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount++;
            return Task.FromResult<int>(user.AccessFailedCount);
        }

        public virtual Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }
            bool result = (from r in this._roleStore.DbEntitySet
                where r.Name.ToUpper() == roleName.ToUpper()
                where r.Users.Any<TUserRole>(ur => ur.UserId.Equals(user.Id))
                select r).Count<TRole>() > 0;
            return Task.FromResult<bool>(result);
        }

        public virtual Task RemoveClaimAsync(TUser user, Claim claim)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            foreach (TUserClaim local in (from uc in user.Claims
                where (uc.ClaimValue == claim.Value) && (uc.ClaimType == claim.Type)
                select uc).ToList<TUserClaim>())
            {
                user.Claims.Remove(local);
            }
            foreach (TUserClaim local2 in from uc in this._userClaims
                where (uc.UserId.Equals(user.Id) && (uc.ClaimValue == claim.Value)) && (uc.ClaimType == claim.Type)
                select uc)
            {
                this._userClaims.Remove(local2);
            }
            return Task.FromResult<int>(0);
        }

        public virtual Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            Func<TUserRole, bool> predicate = null;
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }
            TRole roleEntity = this._roleStore.DbEntitySet.SingleOrDefault<TRole>(r => r.Name.ToUpper() == roleName.ToUpper());
            if (roleEntity != null)
            {
                if (predicate == null)
                {
                    predicate = r => roleEntity.Id.Equals(r.RoleId);
                }
                TUserRole item = user.Roles.FirstOrDefault<TUserRole>(predicate);
                if (item != null)
                {
                    user.Roles.Remove(item);
                    roleEntity.Users.Remove(item);
                }
            }
            return Task.FromResult<int>(0);
        }

        public virtual Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            string provider = login.LoginProvider;
            string key = login.ProviderKey;
            TUserLogin item = user.Logins.SingleOrDefault<TUserLogin>(l => (l.LoginProvider == provider) && (l.ProviderKey == key));
            if (item != null)
            {
                user.Logins.Remove(item);
            }
            return Task.FromResult<int>(0);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount = 0;
            return Task.FromResult<int>(0);
        }

        private async Task SaveChanges()
        {
            if (!((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).AutoSaveChanges)
            {
                return;
                //this.<>1__state = -1;
            }
            await ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).Context.SaveChangesAsync();
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Email = email;
            return Task.FromResult<int>(0);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult<int>(0);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult<int>(0);
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEndDateUtc = (lockoutEnd == DateTimeOffset.MinValue) ? null : new DateTime?(lockoutEnd.UtcDateTime);
            return Task.FromResult<int>(0);
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult<int>(0);
        }

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PhoneNumber = phoneNumber;
            return Task.FromResult<int>(0);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult<int>(0);
        }

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.SecurityStamp = stamp;
            return Task.FromResult<int>(0);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.TwoFactorEnabled = enabled;
            return Task.FromResult<int>(0);
        }

        private void ThrowIfDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(base.GetType().Name);
            }
        }

        public async virtual Task UpdateAsync(TUser user)
        {
            ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this)._userStore.Update(user);
            await ((UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>) this).SaveChanges();
        }

        public bool AutoSaveChanges { get; set; }

        public DbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public IQueryable<TUser> Users
        {
            get
            {
                return this._userStore.EntitySet;
            }
        }





    }
}

