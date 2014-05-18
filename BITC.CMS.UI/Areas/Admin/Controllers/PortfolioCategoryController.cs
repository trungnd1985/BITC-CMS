using BITC.CMS.Data.Model;
using BITC.CMS.Repository;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Web.Mvc;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    public class PortfolioCategoryController : BitcController
    {
        #region Declaration

        #endregion

        #region Constructor

        public PortfolioCategoryController()
        {
            ModuleID = Guid.Parse("1ACA1046-E50F-4FD8-8110-F05AAA280543");
        }

        #endregion

        #region Method

        #endregion

        #region Action

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Create()
        //{
        //    return View("Details", new BITC.CMS.Data.Model.PortfolioCategory() { Year = DateTime.Now.Year });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Portfolio model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var _unitOfWork = new UnitOfWork())
        //        {
        //            var _repo = _unitOfWork.GetRepository<Portfolio>();
        //            //model.PortfolioImages
        //            model.CreatedBy = HttpContext.User.Identity.Name;
        //            model.CreatedDate = DateTime.Now;
        //            model.ModifiedBy = HttpContext.User.Identity.Name;
        //            model.ModifiedDate = DateTime.Now;
        //            _repo.Insert(model);

        //            if (_unitOfWork.SaveChange() > 0)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                return View("Details", model);
        //            }
        //        }
        //    }

        //    return View("Details", model);
        //}

        //public ActionResult Edit(int id)
        //{
        //    using (var _unitOfWork = new UnitOfWork())
        //    {
        //        var _repo = _unitOfWork.GetRepository<Portfolio>();
        //        var _entity = _repo.SingleOrDefault(i => i.PortfolioID == id);

        //        return View("Details", _entity);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, Portfolio model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.PortfolioID = id;
        //        using (var _unitOfWork = new UnitOfWork())
        //        {
        //            var _repo = _unitOfWork.GetRepository<Portfolio>();

        //            model.ModifiedDate = DateTime.Now;
        //            model.ModifiedBy = HttpContext.User.Identity.Name;
        //            _repo.Update(model);

        //            if (_unitOfWork.SaveChange() > 0)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                return View("Details", model);
        //            }
        //        }
        //    }

        //    return View("Details", model);
        //}

        #region AJAX

        public ActionResult LoadAllPortfolioCategories([DataSourceRequest]DataSourceRequest request)
        {
            using (var _unitOfWork = new UnitOfWork())
            {
                var _repo = _unitOfWork.GetRepository<PortfolioCategory>();
                var _culture = CultureHelper.GetCurrentCulture();
                DataSourceResult _result = _repo.Query(i => i.Culture == _culture)
                    .Where(request.Filters)
                    .Sort(request.Sorts)
                    .Page(request.Page - 1, request.PageSize)
                    .ToDataSourceResult(request);
                return Json(_result);
            }
        }

        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, PortfolioCategory _Portfolio)
        {
            if (ModelState.IsValid)
            {
                using (var _unitOfWork = new UnitOfWork())
                {
                    var _repo = _unitOfWork.GetRepository<PortfolioCategory>();
                    _repo.Delete(_Portfolio);
                }
            }
            return Json(new[] { _Portfolio }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion
    }
}