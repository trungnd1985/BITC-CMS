using BITC.CMS.Data;
using BITC.CMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Repository
{
    public class PageRepository : IRepository<Page>
    {
        BITCEntities _dataContext;

        public PageRepository(BITCEntities context)
        {
            _dataContext = context;
        }

        public void Insert(Page _entity)
        {
            _dataContext.Pages.Add(_entity);
        }

        public void Update(Page _entity)
        {
            _dataContext.Entry(_entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Page _entity)
        {
            _dataContext.Entry<Page>(_entity).State = System.Data.Entity.EntityState.Deleted;
            _dataContext.Pages.Remove(_entity);
        }

        public Page SingleOrDefault(System.Linq.Expressions.Expression<Func<Page, bool>> predicate)
        {
            return _dataContext.Pages.SingleOrDefault(predicate);
        }

        public IQueryable<Page> Query()
        {
            return _dataContext.Pages.AsQueryable();
        }

        public IQueryable<Page> Query(System.Linq.Expressions.Expression<Func<Page, bool>> predicate)
        {
            return _dataContext.Pages.Where(predicate);
        }
    }
}
