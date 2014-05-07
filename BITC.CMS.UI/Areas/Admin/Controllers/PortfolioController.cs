using BITC.CMS.Data.Model;
using BITC.CMS.Repository;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class PortfolioController : BitcController
    {
        #region Declaration

        //private UnitOfWork _unitOfWork = null;

        private const string SESSION_KEY_PORTFOLIO_IMAGES = "PortfolioImages";

        #endregion

        #region Action

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            Session.Remove(SESSION_KEY_PORTFOLIO_IMAGES);
            return View("Details", new BITC.CMS.Data.Model.Portfolio() { Year = DateTime.Now.Year });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Portfolio model)
        {
            if (ModelState.IsValid)
            {
                using (var _unitOfWork = new UnitOfWork())
                {
                    var _repo = _unitOfWork.GetRepository<Portfolio>();
                    //model.PortfolioImages
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
                var _repo = _unitOfWork.GetRepository<Portfolio>();
                var _entity = _repo.SingleOrDefault(i => i.PortfolioID == id);

                return View("Details", _entity);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Portfolio model)
        {
            if (ModelState.IsValid)
            {
                model.PortfolioID = id;
                using (var _unitOfWork = new UnitOfWork())
                {
                    var _repo = _unitOfWork.GetRepository<Portfolio>();

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

        public ActionResult PortfolioImageDetail()
        {
            return View();
        }

        #region AJAX

        public ActionResult LoadAllPortfolios([DataSourceRequest]DataSourceRequest request)
        {
            using (var _unitOfWork = new UnitOfWork())
            {
                var _repo = _unitOfWork.GetRepository<Portfolio>();
                var _culture = CultureHelper.GetCurrentCulture();
                DataSourceResult _result = _repo.Query(i => i.Culture == _culture)
                    .Where(request.Filters)
                    .Sort(request.Sorts)
                    .Page(request.Page - 1, request.PageSize)
                    .ToDataSourceResult(request);
                return Json(_result);
            }
        }

        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, Portfolio _Portfolio)
        {
            if (ModelState.IsValid)
            {
                using (var _unitOfWork = new UnitOfWork())
                {
                    var _repo = _unitOfWork.GetRepository<Portfolio>();
                    _repo.Delete(_Portfolio);
                }
            }
            return Json(new[] { _Portfolio }.ToDataSourceResult(request, ModelState));
        }

        [AjaxOnly]
        [HttpPost]
        [ChildActionOnly]
        public ActionResult SavePortfolioImage(PortfolioImage _img)
        {
            List<PortfolioImage> _lst = new List<PortfolioImage>();

            if (Session[SESSION_KEY_PORTFOLIO_IMAGES] != null)
            {
                _lst = (List<PortfolioImage>)Session[SESSION_KEY_PORTFOLIO_IMAGES];
            }
            
            _lst.Add(_img);

            Session[SESSION_KEY_PORTFOLIO_IMAGES] = _lst;

            return Json(_img);
        }

        public ActionResult UploadPortfolioImage(IEnumerable<HttpPostedFileBase> PortfolioImage)
        {
            if (PortfolioImage != null)
            {
                foreach (var file in PortfolioImage)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // The files are not actually saved in this demo
                    file.SaveAs(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult RemovePortfolioImage(string[] PortfolioImage)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (PortfolioImage != null)
            {
                foreach (var fullName in PortfolioImage)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        #endregion

        #endregion
    }
}