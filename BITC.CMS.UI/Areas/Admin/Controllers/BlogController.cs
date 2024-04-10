using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using BITC.CMS.Service;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class BlogController : BitcController
    {
        #region Declaration

        private readonly IBlogService _service;
        private IUnitOfWorkAsync _unitOfWork;

        #endregion

        #region Constructor

        public BlogController(IBlogService service, IUnitOfWorkAsync uow)
        {
            _unitOfWork = uow;
            _service = service;
        }

        #endregion

        //
        // GET: /Admin/Blog/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("Details", new BITC.CMS.Data.Entity.BlogEntry() { PublishDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogEntry model)
        {
            if (ModelState.IsValid)
            {
                _service.Insert(model);

                if (_unitOfWork.SaveChanges() > 0)
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
            var _entity = _service.GetBlogEntry(id);

            foreach (var item in _entity.BlogTags)
            {
                _entity.SelectedTags += item.TagName + ",";
            }

            return View("Details", _entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BlogEntry model)
        {
            if (ModelState.IsValid)
            {
                _service.Update(id, model);

                if (_unitOfWork.SaveChanges() > 0)
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
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, BlogEntry _tag)
        {
            _service.Delete(_tag);
            _unitOfWork.SaveChanges();
            return Json(new[] { _tag }.ToDataSourceResult(request, ModelState));
        }

        #endregion
    }
}