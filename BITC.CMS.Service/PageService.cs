using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using BITC.Web.Library;
using System;
using System.Collections.Generic;
using System.Data.Entity.Hierarchy;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BITC.CMS.Service
{
    public class PageService : BaseService<Page>, IPageService
    {
        private readonly IRepositoryAsync<Page> _repository;

        public PageService(IRepositoryAsync<Page> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Page GetPage(int id)
        {
            return _repository.Find(id);
        }

        public override void Insert(Page entity)
        {
            entity.Culture = CultureHelper.GetCurrentCulture();
            entity.CreatedBy = HttpContext.Current.User.Identity.Name;
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedBy = HttpContext.Current.User.Identity.Name;
            entity.ModifiedDate = DateTime.Now;

            Page _parentPage = null;

            if (entity.ParentID.HasValue)
            {
                _parentPage = _repository.Query(i => i.PageID == entity.ParentID.Value).Include(i => i.Children).Select().FirstOrDefault();
            }

            if (_parentPage != null)
            {
                entity.Path = _parentPage.Path.GetDescendant(_parentPage.Children.LastOrDefault() != null ? _parentPage.Children.LastOrDefault().Path : null, null);
            }
            else
            {
                entity.Path = HierarchyId.GetRoot();
            }

            base.Insert(entity);
        }


        public void Update(int id, Page model)
        {
            model.PageID = id;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = HttpContext.Current.User.Identity.Name;

            Page _parentPage = null;

            if (model.ParentID.HasValue)
            {
                _parentPage = _repository.Query(i => i.PageID == model.ParentID.Value).Include(i => i.Children).Select().FirstOrDefault();
            }

            if (_parentPage != null)
            {
                model.Path = _parentPage.Path.GetDescendant(_parentPage.Children.LastOrDefault() != null ? _parentPage.Children.LastOrDefault().Path : null, null);
            }
            else
            {
                model.Path = HierarchyId.GetRoot();
            }

            base.Update(model);
        }

        public IQueryable<Page> FindByCulture(string _culture)
        {
            return _repository.Queryable(i => i.Culture == _culture);
        }

        public HierarchyId GetCurrentPath(int? id)
        {
            return _repository.Query(i => id.HasValue && i.PageID == id.Value).Select(i => i.Path).FirstOrDefault();
        }


        public IQueryable<Page> GetDataForPageParentDropDownList(int? id)
        {
            var currentPath = GetCurrentPath(id);
            return _repository.Queryable(i => currentPath != null && !i.Path.IsDescendantOf(currentPath));
        }
    }
}
