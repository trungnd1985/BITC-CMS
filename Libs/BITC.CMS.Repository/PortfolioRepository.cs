using BITC.CMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Repository
{
    public class PortfolioRepository : IRepository<Portfolio>
    {
        BITCEntities _dataContext;

        public PortfolioRepository(BITCEntities context)
        {
            _dataContext = context;
        }

        public Portfolio SingleOrDefault(System.Linq.Expressions.Expression<Func<Portfolio, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Portfolio> Query()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Portfolio> Query(System.Linq.Expressions.Expression<Func<Portfolio, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Insert(Portfolio _entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Portfolio _entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(Portfolio _entity)
        {
            throw new NotImplementedException();
        }
    }
}
