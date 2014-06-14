using BITC.CMS.Service;
using BITC.Library.Pattern;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using BITC.Web.Library;
using BITC.CMS.Data.Entity;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectCategoryController : BitcController
    {
        #region Declaration

        private readonly IProjectCategoryService _service;
        private readonly IUnitOfWorkAsync _unitOfWork;

        #endregion

        #region Constructor

        public ProjectCategoryController(IProjectCategoryService service, IUnitOfWorkAsync uow)
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
            return View("Details", new ProjectCategory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(ProjectCategory model)
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
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, ProjectCategory _category)
        {
            _service.Delete(_category);
            _unitOfWork.SaveChanges();
            return Json(new[] { _category }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion
    }
}