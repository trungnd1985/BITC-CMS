using BITC.CMS.Data;
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
            throw new NotImplementedException();
        }

        public void Update(Page _entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Page _entity)
        {
            throw new NotImplementedException();
        }

        public Page SingleOrDefault(System.Linq.Expressions.Expression<Func<Page, bool>> predicate)
        {
            return _dataContext.Pages.SingleOrDefault(predicate);
        }
    }
}
