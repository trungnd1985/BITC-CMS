namespace BITC.CMS.Identity.EntityFramework
{
    using BITC.CMS.Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class RoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey>, IRoleStore<TRole, TKey>, IDisposable where TRole: IdentityRole<TKey, TUserRole>, new() where TUserRole: IdentityUserRole<TKey>, new()
    {
        private bool _disposed;
        private EntityStore<TRole> _roleStore;

        public RoleStore(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.Context = context;
            this._roleStore = new EntityStore<TRole>(context);
        }

        public async virtual Task CreateAsync(TRole role)
        {
            ((RoleStore<TRole, TKey, TUserRole>) this).ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            ((RoleStore<TRole, TKey, TUserRole>) this)._roleStore.Create(role);
            await ((RoleStore<TRole, TKey, TUserRole>) this).Context.SaveChangesAsync();
        }

        public async virtual Task DeleteAsync(TRole role)
        {
            ((RoleStore<TRole, TKey, TUserRole>) this).ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            ((RoleStore<TRole, TKey, TUserRole>) this)._roleStore.Delete(role);
            await ((RoleStore<TRole, TKey, TUserRole>) this).Context.SaveChangesAsync();
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
            this._roleStore = null;
        }

        public Task<TRole> FindByIdAsync(TKey roleId)
        {
            this.ThrowIfDisposed();
            return this._roleStore.GetByIdAsync(roleId);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            this.ThrowIfDisposed();
            return this._roleStore.EntitySet.FirstOrDefaultAsync<TRole>(((Expression<Func<TRole, bool>>) (u => (u.Name.ToUpper() == roleName.ToUpper()))));
        }

        private void ThrowIfDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(base.GetType().Name);
            }
        }

        public async virtual Task UpdateAsync(TRole role)
        {
            ((RoleStore<TRole, TKey, TUserRole>) this).ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            ((RoleStore<TRole, TKey, TUserRole>) this)._roleStore.Update(role);
            await ((RoleStore<TRole, TKey, TUserRole>) this).Context.SaveChangesAsync();
        }

        public DbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public IQueryable<TRole> Roles
        {
            get
            {
                return this._roleStore.EntitySet;
            }
        }



    }
}

