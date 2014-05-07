using BITC.CMS.Data.Model;
using BITC.CMS.Repository;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Web;
using System.Web.Mvc;

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

                    model.CreatedBy = HttpContext.User.Identity.Name;
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedBy = HttpContext.User.Identity.Name;
                    model.ModifiedDate = DateTime.Now;

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
            using (var _unitOfWork = new UnitOfWork())
            {
                var _repo = _unitOfWork.GetRepository<Page>();
                var _entity = _repo.SingleOrDefault(i => i.PageID == id);

                return View("Details", _entity);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Page model)
        {
            if (ModelState.IsValid)
            {
                model.PageID = id;
                using (var _unitOfWork = new UnitOfWork())
                {
                    var _repo = _unitOfWork.GetRepository<Page>();

                    model.ModifiedDate = DateTime.Now;
                    model.ModifiedBy = HttpContext.User.Identity.Name;

                    if (_repo.Update(model) > 0)
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

        #region AJAX

        public ActionResult LoadAllPages([DataSourceRequest]DataSourceRequest request)
        {
            using (var _unitOfWork = new UnitOfWork())
            {
                var _repo = _unitOfWork.GetRepository<Page>();
                var _culture = CultureHelper.GetCurrentCulture();
                DataSourceResult _result = _repo.Query(i => i.Culture == _culture)
                    .Where(request.Filters)
                    .Sort(request.Sorts)
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