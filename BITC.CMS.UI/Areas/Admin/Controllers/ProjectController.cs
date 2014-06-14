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
using BITC.CMS.Data.Entity;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectController : BitcController
    {
        #region Declaration

        private readonly IProjectService _service;
        private readonly IUnitOfWorkAsync _unitOfWork;

        #endregion

        #region Constructor

        public ProjectController(IProjectService service, IUnitOfWorkAsync uow)
        {
            _service = service;
            _unitOfWork = uow;
        }

        #endregion

        #region Action

        // GET: Admin/Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("Details", new Project());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project _model)
        {
            if (ModelState.IsValid)
            {
                _service.Insert(_model);

                if (_unitOfWork.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Details", _model);
                }
            }

            return View("Details", _model);
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
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, Project _tag)
        {
            _service.Delete(_tag);
            _unitOfWork.SaveChanges();
            return Json(new[] { _tag }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion
    }
}