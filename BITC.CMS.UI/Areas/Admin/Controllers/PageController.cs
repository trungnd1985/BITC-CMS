using BITC.CMS.Data;
using BITC.CMS.Repository;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc;
using BITC.CMS.UI.Areas.Admin.Models;
using BITC.CMS.Data.Model;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class PageController : BitcController
    {
        #region Declaration

        //private UnitOfWork _unitOfWork = null;

        #endregion

        #region Action

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View("Details", new BITC.CMS.Data.Model.Page());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Page model)
        {
            if (ModelState.IsValid)
            {
                using (var _unitOfWork = new UnitOfWork())
                {
                    var _repo = _unitOfWork.GetRepository<Page>();
                    if (_repo.Insert(model) > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Details", model);
                    }
                }
            }

            return View("Details", model);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        #region AJAX

        public ActionResult LoadAllPages([DataSourceRequest]DataSourceRequest request)
        {
            using (var _unitOfWork = new UnitOfWork())
            {
                var _repo = _unitOfWork.GetRepository<Page>();
                DataSourceResult _result = _repo.Query()
                    .Where(request.Filters)
                    .Sort(request.Sorts.Count > 0 ? request.Sorts : new List<SortDescriptor>() { new SortDescriptor("PageTitle", System.ComponentModel.ListSortDirection.Ascending) })
                    .Page(request.Page - 1, request.PageSize)
                    .ToDataSourceResult(request);
                return Json(_result);
            }
        }

        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, Page _page)
        {
            if (ModelState.IsValid)
            {
                using (var _unitOfWork = new UnitOfWork())
                {
                    var _repo = _unitOfWork.GetRepository<Page>();
                    _repo.Delete(_page);
                }
            }
            return Json(new[] { _page }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion
    }
}