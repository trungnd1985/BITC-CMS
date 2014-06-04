using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

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

                var arrTagSelected = model.SelectedTags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var lstTag = _blogTagRepo.Queryable(i => model.SelectedTags.Contains(i.TagName));

                string currentTagName;

                for (int i = 0; i < arrTagSelected.Length; i++)
                {
                    currentTagName = arrTagSelected[i];

                    var newTag = lstTag.FirstOrDefault(t => t.TagName == currentTagName);

                    if (newTag == null)
                    {
                        newTag = new BlogTag();
                        newTag.TagName = arrTagSelected[i];
                        newTag.Culture = CultureHelper.GetCurrentCulture();
                        newTag.Slug = newTag.TagName.ToSlug();
                        newTag.BlogEntries.Add(model);
                        _blogTagRepo.Insert(newTag);
                    }
                    else
                    {
                        model.BlogTags.Add(newTag);
                    }
                }

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

            foreach (var item in _entity.BlogTags)
            {
                _entity.SelectedTags += item.TagName + ",";
            }

            return View("Details", _entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BlogEntry model)
        {
            if (ModelState.IsValid)
            {
                var currentEntry = _repo.Query(i => i.BlogEntryID == id).Include(i => i.BlogTags).Select().FirstOrDefault();

                currentEntry.ModifiedBy = HttpContext.User.Identity.Name;
                currentEntry.ModifiedDate = DateTime.Now;
                currentEntry.BlogTags.Clear();

                var arrTagSelected = model.SelectedTags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var lstTag = _blogTagRepo.Queryable(i => model.SelectedTags.Contains(i.TagName));

                string currentTagName;

                for (int i = 0; i < arrTagSelected.Length; i++)
                {
                    currentTagName = arrTagSelected[i];

                    var newTag = lstTag.FirstOrDefault(t => t.TagName == currentTagName);

                    if (newTag == null)
                    {
                        newTag = new BlogTag();
                        newTag.TagName = arrTagSelected[i];
                        newTag.Culture = CultureHelper.GetCurrentCulture();
                        newTag.Slug = newTag.TagName.ToSlug();
                        newTag.BlogEntries.Add(currentEntry);
                        _blogTagRepo.Insert(newTag);
                    }
                    else
                    {
                        currentEntry.BlogTags.Add(newTag);
                    }
                }
                //AuthenticationManager.

                _repo.Update(currentEntry);

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