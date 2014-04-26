using BITC.CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Repository
{
    public interface IRepository<T>
    {
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        void Insert(T _entity);
        void Update(T _entity);
        void Delete(T _entity);
    }
}
