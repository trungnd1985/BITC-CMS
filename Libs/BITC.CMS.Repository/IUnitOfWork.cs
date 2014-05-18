using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChange();
        IRepository<T> GetRepository<T>();
        void Dispose(bool disposing);
    }
}
