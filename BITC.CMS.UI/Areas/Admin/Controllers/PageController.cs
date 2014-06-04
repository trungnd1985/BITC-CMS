using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity.Hierarchy;
using BITC.CMS.UI.Areas.Admin.Models;
using System.Collections.Generic;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class PageController : BitcController
    {
        #region Declaration

        private readonly IRepositoryAsync<Page> _pageRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        #endregion

        #region Constructor

        public PageController(IUnitOfWorkAsync unitOfWork, IRepositoryAsync<Page> _repository)
        {
            _unitOfWorkAsync = unitOfWork;
            _pageRepository = _repository;
        }

        #endregion

        #region Action

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            ViewBag.PageList = GetPageList(null, null);
            return View("Details", new BITC.CMS.Data.Entity.Page());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Page model)
        {
            if (ModelState.IsValid)
            {
                model.Culture = CultureHelper.GetCurrentCulture();
                model.CreatedBy = HttpContext.User.Identity.Name;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = HttpContext.User.Identity.Name;
                model.ModifiedDate = DateTime.Now;

                Page _parentPage = null;

                if (model.ParentID.HasValue)
                {
                    _parentPage = _pageRepository.Query(i => i.PageID == model.ParentID.Value).Include(i => i.Children).Select().FirstOrDefault();
                }
                

                if (_parentPage != null)
                {
                    model.Path = _parentPage.Path.GetDescendant(_parentPage.Children.LastOrDefault() != null ? _parentPage.Children.LastOrDefault().Path : null, null);
                }
                else
                {
                    model.Path = HierarchyId.GetRoot();
                }

                _pageRepository.Insert(model);

                if (_unitOfWorkAsync.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Details", model);
                }
            }

            return View("Details", model);
        }

        public ActionResult Edit(int id)
        {
            var _entity = _pageRepository.Queryable().SingleOrDefault(i => i.PageID == id);
            ViewBag.PageList = GetPageList(_entity.ParentID, _entity.Path);
            return View("Details", _entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Page model)
        {
            //ViewBag.PageList = GetPageList(model.ParentID, model.Path);

            if (ModelState.IsValid)
            {
                model.PageID = id;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = HttpContext.User.Identity.Name;

                Page _parentPage = null;

                if (model.ParentID.HasValue)
                {
                    _parentPage = _pageRepository.Query(i => i.PageID == model.ParentID.Value).Include(i => i.Children).Select().FirstOrDefault();
                }

                if (_parentPage != null)
                {
                    model.Path = _parentPage.Path.GetDescendant(_parentPage.Children.LastOrDefault() != null ? _parentPage.Children.LastOrDefault().Path : null, null);
                }
                else
                {
                    model.Path = HierarchyId.GetRoot();
                }

                _pageRepository.Update(model);

                if (_unitOfWorkAsync.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Details", model);
                }
            }

            return View("Details", model);
        }

        #region AJAX

        [AjaxOnly]
        public ActionResult LoadAllPages([DataSourceRequest]DataSourceRequest request)
        {
            var _culture = CultureHelper.GetCurrentCulture();
            ;
            DataSourceResult _result = _pageRepository.Query(i => i.Culture == _culture)
                .Select(i => new PageIndexModel { PageID = i.PageID, PageTitle = i.PageTitle, CreatedDate = i.CreatedDate, Url = i.Url, CreatedBy = i.CreatedBy, SortOrder = i.SortOrder })
                .AsQueryable()
                .Where(request.Filters)
                .Sort(request.Sorts)
                .Page(request.Page - 1, request.PageSize)
                .ToDataSourceResult(request);
            return Json(_result);
        }

        [AjaxOnly]
        public ActionResult GetUrl(int? parentId, string title)
        {
            var _parent = _pageRepository.Queryable(i => i.PageID == parentId).FirstOrDefault();
            var _path = title.ToSlug();
            var _result = (_parent != null && !string.IsNullOrEmpty(_parent.Url) ? (_parent.Url + "/") : string.Empty) + _path;

            return Json(_result);
        }

        [AjaxOnly]
        public ActionResult GetTemplateContent(string _template)
        {
            var _filePath = HttpContext.Server.MapPath("~/Theme/" + BITC.Web.Library.Utility.GetTheme() + "/Views/Shared/" + _template + ".cshtml");
            return Json(System.IO.File.ReadAllText(_filePath));
        }

        [AjaxOnly]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, Page _page)
        {
            _pageRepository.Delete(_page);
            _unitOfWorkAsync.SaveChanges();
            return Json(new[] { _page }.ToDataSourceResult(request, ModelState));
        }

        [AjaxOnly]
        public ActionResult GetDataForPageParentDropDownList(int? id)
        {
            var currentPath = _pageRepository.Query(i => id.HasValue && i.PageID == id.Value).Select(i => i.Path).FirstOrDefault();
            var lst = _pageRepository.Queryable(i => currentPath != null && !i.Path.IsDescendantOf(currentPath));

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Method

        private IEnumerable<SelectListItem> GetPageList(int? parentId, HierarchyId _path)
        {
            IQueryable<Page> lst;

            var _culture = CultureHelper.GetCurrentCulture();

            if (_path == null)
            {
                lst = _pageRepository.Queryable(i => i.Culture == _culture).OrderBy(i => i.Path);
            }
            else
            {
                lst = _pageRepository.Queryable(i => i.Culture == _culture && !i.Path.IsDescendantOf(_path)).OrderBy(i => i.Path);
            }

            if (lst != null)
            {
                foreach (var item in lst)
                {
                    yield return new SelectListItem() { Text = GeneratePageLevel(item.Path.ToString()) + item.PageTitle, Value = item.PageID.ToString(), Selected = item.PageID == parentId };
                }
            }
        }

        private string GeneratePageLevel(string _path)
        {
            var seperatorCount = _path.Count(i => i.Equals('/'));
            string _result = string.Empty;

            if (seperatorCount > 1)
            {
                for (int i = 0; i < seperatorCount; i++)
                {
                    _result += "- ";
                }
            }

            return _result;
        }

        #endregion
    }
}