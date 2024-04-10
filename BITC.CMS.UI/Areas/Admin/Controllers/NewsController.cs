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
    public class NewsController : BitcController
    {
        #region Declaration

        private readonly INewsService _service;
        private readonly INewsCategoryService _categoryService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        #endregion

        #region Constructor

        public NewsController(INewsService service, INewsCategoryService categoryService, IUnitOfWorkAsync uow)
        {
            _service = service;
            _categoryService = categoryService;
            _unitOfWork = uow;
        }

        #endregion

        #region Action

        //
        // GET: /Admin/Blog/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("Details", new BITC.CMS.Data.Entity.News() { PublishDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News model)
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
            var _entity = _service.GetNews(id);

            foreach (var item in _entity.NewsCategories)
            {
                _entity.NewsCategoriesID.Add(item.NewsCategoryID);
            }

            return View("Details", _entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, News model)
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
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, News _tag)
        {
            _service.Delete(_tag);
            _unitOfWork.SaveChanges();
            return Json(new[] { _tag }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion
    }
}