using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.Library.Pattern
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : IObjectState;
        void BeginTransaction(IsolationLevel isolateLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }
}
