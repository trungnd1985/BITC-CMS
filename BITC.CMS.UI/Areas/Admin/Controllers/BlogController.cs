using BITC.Web.Library.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using BITC.CMS.Data.Entity;
using Kendo.Mvc.UI;
using BITC.Web.Library;
using BITC.Library.Pattern;
using Microsoft.AspNet.Identity;
using BITC.CMS.UI.Models;
using System.Net;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class BlogController : BitcController
    {
        #region Declaration

        private IRepositoryAsync<BlogEntry> _repo;
        private IUnitOfWorkAsync _unitOfWork;
        private IRepositoryAsync<BlogTag> _blogTagRepo;
        #endregion

        #region Constructor

        public BlogController(IRepositoryAsync<BlogEntry> repo, IUnitOfWorkAsync uow, IRepositoryAsync<BlogTag> tagRepo)
        {
            _repo = repo;
            _unitOfWork = uow;
            _blogTagRepo = tagRepo;
        }

        #endregion

        //
        // GET: /Admin/Blog/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("Details", new BITC.CMS.Data.Entity.BlogEntry());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogEntry model)
        {
            if (ModelState.IsValid)
            {
                model.Culture = CultureHelper.GetCurrentCulture();
                model.CreatedBy = User.Identity.Name;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = User.Identity.Name;
                model.ModifiedDate = DateTime.Now;

                _repo.Insert(model);

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
            var _entity = _repo.Query().Include(m => m.BlogTags).Select().SingleOrDefault(i => i.BlogEntryID == id);
            return View("Details", _entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BlogEntry model)
        {
            if (ModelState.IsValid)
            {
                model.BlogEntryID = id;
                model.ModifiedDate = DateTime.Now;

                foreach (var item in model.SelectedTags)
                {
                   //model.
                    //model.BlogTags.Add(_blogTagRepo.SelectQuery(item);
                }

                model.ModifiedBy = User.Identity.Name;
                //AuthenticationManager.

                _repo.Update(model);

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
            DataSourceResult _result = _repo.Queryable(i => i.Culture == _culture)
                .Where(request.Filters)
                .Sort(request.Sorts)
                .Page(request.Page - 1, request.PageSize)
                .ToDataSourceResult(request);
            return Json(_result);
        }

        [AjaxOnly]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, BlogEntry _tag)
        {
            _repo.Delete(_tag);
            _unitOfWork.SaveChanges();
            return Json(new[] { _tag }.ToDataSourceResult(request, ModelState));
        }

        #endregion
    }
}