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

        public int Insert(Page _entity)
        {
            _dataContext.Pages.Add(_entity);
            return _dataContext.SaveChanges();
        }

        public int Update(Page _entity)
        {
            var oldEntity = _dataContext.Pages.FirstOrDefault(i => i.PageID == _entity.PageID);

            //_dataContext.Entry(_entity).State = System.Data.Entity.EntityState.Modified;
            if (oldEntity != null)
            {
                oldEntity.PageTitle = _entity.PageTitle;
                oldEntity.Description = _entity.Description;
                oldEntity.Body = _entity.Body;
                oldEntity.Keywords = _entity.Keywords;
                oldEntity.ModifiedBy = _entity.ModifiedBy;
                oldEntity.ModifiedDate = DateTime.Now;
                oldEntity.SortOrder = _entity.SortOrder;
                oldEntity.Template = _entity.Template;
                oldEntity.Url = _entity.Url;
                oldEntity.Inactive = _entity.Inactive;
            }
            else
            {
                _dataContext.Pages.Add(_entity);
            }

            return _dataContext.SaveChanges();
        }

        public int Delete(Page _entity)
        {
            _dataContext.Entry<Page>(_entity).State = System.Data.Entity.EntityState.Deleted;
            _dataContext.Pages.Remove(_entity);
            return _dataContext.SaveChanges();
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
