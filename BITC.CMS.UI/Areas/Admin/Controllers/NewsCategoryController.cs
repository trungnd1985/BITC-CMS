using BITC.CMS.Data.Entity;
using BITC.CMS.Service;
using BITC.Library.Pattern;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class NewsCategoryController : BitcController
    {

        #region Declaration

        private readonly INewsCategoryService _service;
        private readonly IUnitOfWorkAsync _unitOfWork;

        #endregion

        #region Constructor

        public NewsCategoryController(INewsCategoryService service, IUnitOfWorkAsync uow)
        {
            _service = service;
            _unitOfWork = uow;
        }

        #endregion

        #region Action

        // GET: Admin/ProjectCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("Details", new NewsCategory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(NewsCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = HttpContext.User.Identity.Name;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = HttpContext.User.Identity.Name;
                model.ModifiedDate = DateTime.Now;
                model.Culture = CultureHelper.GetCurrentCulture();

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
            var _entity = _service.Find(id);

            return View("Details", _entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, NewsCategory model)
        {
            //if (ModelState.IsValid)
            //{
            //    _service.Update(id, model);

            //    if (_unitOfWork.SaveChanges() > 0)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return View("Details", model);
            //    }
            //}

            return View("Details", model);
        }

        #region AJAX

        [AjaxOnly]
        public ActionResult GetNewsCategories()
        {
            var _culture = CultureHelper.GetCurrentCulture();

            return Json(_service.FindByCulture(_culture).Where(i => i.Inactive == false), JsonRequestBehavior.AllowGet);
        }

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
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, NewsCategory _category)
        {
            _service.Delete(_category);
            _unitOfWork.SaveChanges();
            return Json(new[] { _category }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion
    }
}