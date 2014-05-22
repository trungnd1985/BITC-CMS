using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class PageController : BitcController
    {
        #region Declaration

        private readonly IRepositoryAsync<Page> _pageRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        #endregion

        #region Constructor

        public PageController(IUnitOfWorkAsync unitOfWork, IRepositoryAsync<Page> _repository)
        {
            _unitOfWorkAsync = unitOfWork;
            _pageRepository = _repository;
        }

        #endregion

        #region Action

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View("Details", new BITC.CMS.Data.Entity.Page());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Page model)
        {
            if (ModelState.IsValid)
            {
                model.Culture = CultureHelper.GetCurrentCulture();
                model.CreatedBy = HttpContext.User.Identity.Name;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = HttpContext.User.Identity.Name;
                model.ModifiedDate = DateTime.Now;

                _pageRepository.Insert(model);

                if (_unitOfWorkAsync.SaveChanges() > 0)
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
            var _entity = _pageRepository.Queryable().SingleOrDefault(i => i.PageID == id);

            return View("Details", _entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Page model)
        {
            if (ModelState.IsValid)
            {
                model.PageID = id;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = HttpContext.User.Identity.Name;
                _pageRepository.Update(model);

                if (_unitOfWorkAsync.SaveChanges() > 0)
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
        public ActionResult LoadAllPages([DataSourceRequest]DataSourceRequest request)
        {
            var _culture = CultureHelper.GetCurrentCulture();
            DataSourceResult _result = _pageRepository.Queryable(i => i.Culture == _culture)
                .Where(request.Filters)
                .Sort(request.Sorts)
                .Page(request.Page - 1, request.PageSize)
                .ToDataSourceResult(request);
            return Json(_result);
        }

        [AjaxOnly]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, Page _page)
        {
            _pageRepository.Delete(_page);
            _unitOfWorkAsync.SaveChanges();
            return Json(new[] { _page }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion
    }
}