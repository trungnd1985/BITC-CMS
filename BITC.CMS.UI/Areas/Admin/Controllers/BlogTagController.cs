using BITC.CMS.Data.Entity;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using BITC.Web.Library.Mvc;
using BITC.Library.Pattern;
using BITC.Web.Library;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class BlogTagController : BitcController
    {
        #region Declaration

        private IRepositoryAsync<BlogTag> _repo;
        private IUnitOfWorkAsync _unitOfWork;

        #endregion

        #region Constructor

        public BlogTagController(IRepositoryAsync<BlogTag> repo, IUnitOfWorkAsync uow)
        {
            _repo = repo;
            _unitOfWork = uow;
        }

        #endregion

        #region Action

        public ActionResult Index()
        {
            return View();
        }

        #region AJAX

        [AjaxOnly]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var _culture = CultureHelper.GetCurrentCulture();
            DataSourceResult _result = _repo.Queryable(i => i.Culture == _culture)
                .Where(request.Filters)
                .Sort(request.Sorts)
                .Page(request.Page - 1, request.PageSize)
                .ToDataSourceResult(request);
            return Json(_result);
        }

        [AjaxOnly]
        public ActionResult LoadAllBlogTags()
        {
            var _culture = CultureHelper.GetCurrentCulture();
            return Json(_repo.Queryable(i => i.Culture == _culture), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, BlogTag _tag)
        {
            if (_tag != null && ModelState.IsValid)
            {
                _tag.Culture = CultureHelper.GetCurrentCulture();
                _tag.Slug = _tag.TagName.ToSlug();
                _repo.Insert(_tag);
                _unitOfWork.SaveChanges();
            }

            return Json(new[] { _tag }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, BlogTag _tag)
        {
            if (_tag != null && ModelState.IsValid)
            {
                _tag.Slug = _tag.TagName.ToSlug();
                _repo.Update(_tag);
                _unitOfWork.SaveChanges();
            }

            return Json(new[] { _tag }.ToDataSourceResult(request, ModelState));
        }

        [AjaxOnly]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, BlogTag _tag)
        {
            _repo.Delete(_tag);
            _unitOfWork.SaveChanges();
            return Json(new[] { _tag }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion        
    }
}