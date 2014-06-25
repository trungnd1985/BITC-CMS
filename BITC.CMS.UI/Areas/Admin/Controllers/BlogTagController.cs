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
using BITC.CMS.Service;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class BlogTagController : BitcController
    {
        #region Declaration

        private IBlogTagService _service;
        private IUnitOfWorkAsync _unitOfWork;

        #endregion

        #region Constructor

        public BlogTagController(IBlogTagService service, IUnitOfWorkAsync uow)
        {
            _service = service;
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
            DataSourceResult _result = _service.FindByCulture(_culture)
                .Where(request.Filters)
                .Sort(request.Sorts)
                .Page(request.Page - 1, request.PageSize)
                .ToDataSourceResult(request);
            return Json(_result);
        }

        [AjaxOnly]
        public ActionResult LoadAllBlogTags(string term)
        {
            var _culture = CultureHelper.GetCurrentCulture();
            return Json(_service.LoadAllTagsByTerm(_culture, term).Select(i => i.TagName), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, BlogTag _tag)
        {
            if (_tag != null && ModelState.IsValid)
            {
                _tag.Culture = CultureHelper.GetCurrentCulture();
                _tag.Slug = _tag.TagName.ToSlug();
                _service.Insert(_tag);
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
                _service.Update(_tag);
                _unitOfWork.SaveChanges();
            }

            return Json(new[] { _tag }.ToDataSourceResult(request, ModelState));
        }

        [AjaxOnly]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, BlogTag _tag)
        {
            _service.Delete(_tag);
            _unitOfWork.SaveChanges();
            return Json(new[] { _tag }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion
    }
}