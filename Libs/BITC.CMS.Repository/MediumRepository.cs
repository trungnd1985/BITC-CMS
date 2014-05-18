using BITC.CMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Repository
{
    public class MediumRepository : IRepository<Medium>
    {
        BITCEntities _dataContext;

        public MediumRepository(BITCEntities context)
        {
            _dataContext = context;
        }

        public Medium SingleOrDefault(System.Linq.Expressions.Expression<Func<Medium, bool>> predicate)
        {
            return _dataContext.Media.FirstOrDefault(predicate);
        }

        public IQueryable<Medium> Query()
        {
            return _dataContext.Media.AsQueryable();
        }

        public IQueryable<Medium> Query(System.Linq.Expressions.Expression<Func<Medium, bool>> predicate)
        {
            return Query().Where(predicate).AsQueryable();
        }

        public void Insert(Medium _entity)
        {
            _dataContext.Media.Add(_entity);
        }

        public void Update(Medium _entity)
        {
            _dataContext.Entry(_entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Medium _entity)
        {
            _dataContext.Entry(_entity).State = System.Data.Entity.EntityState.Deleted;
            _dataContext.Media.Remove(_entity);
        }
    }
}
