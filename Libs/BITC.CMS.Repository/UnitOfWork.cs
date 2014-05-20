using BITC.CMS.Data;
using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private DbTransaction _transaction;
        private bool _disposed;
        BITCEntities _dataContext = null;
        private ObjectContext _objectContext;

        public UnitOfWork()
        {
            _dataContext = new BITCEntities();
        }

        public int SaveChange()
        {
            return _dataContext.SaveChanges();
        }

        public IRepository<T> GetRepository<T>()
        {
            var repositoryType = typeof(IRepository<>);

            return (IRepository<T>)Activator.CreateInstance(Type.GetType("BITC.CMS.Repository." + typeof(T).Name + "Repository"), _dataContext);
        }

        //public void BeginTransaction()
        //{
        //    _objectContext = ((IObjectContextAdapter)_dataContext).ObjectContext;
        //    if (_objectContext.Connection.State != ConnectionState.Open)
        //    {
        //        _objectContext.Connection.Open();
        //        _transaction = _objectContext.Connection.BeginTransaction();
        //    }
        //}

        //public bool Commit()
        //{
        //    _transaction.Commit();
        //    return true;
        //}

        //public void Rollback()
        //{
        //    _transaction.Rollback();
        //    ((DbContext)_dataContext).SyncObjectsStatePostCommit();
        //}


        public void Dispose()
        {
            if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
                _objectContext.Connection.Close();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _dataContext.Dispose();
            _disposed = true;
        }
    }
}
