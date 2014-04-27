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

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    public class PageController : BitcController
    {
        #region Action

        public ActionResult Index()
        {
            return View();
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
                    .Sort(request.Sorts)                    
                    .Page(request.Page, request.PageSize)
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