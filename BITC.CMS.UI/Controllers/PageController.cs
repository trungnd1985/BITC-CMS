using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using BITC.Web.Library.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace BITC.CMS.UI.Controllers
{
    public class PageController : BitcController
    {
        private readonly IRepositoryAsync<Page> _pageRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PageController(IUnitOfWorkAsync unitOfWork, IRepositoryAsync<Page> _repository)
        {
            _unitOfWorkAsync = unitOfWork;
            _pageRepository = _repository;
        }


        public ActionResult Index(string url)
        {
            Page _model = null;

            if (string.IsNullOrEmpty(url))
            {
                url = "";

                _model = _pageRepository.Query(i => i.Url == url).Select().FirstOrDefault();
            }
            else
            {
                var lst = _pageRepository.Query(i => url.Contains(i.Url)).Include(i => i.Parent).Select().OrderByDescending(i => i.Expression.Count());

                foreach (var item in lst)
                {
                    if (Regex.IsMatch(item.Expression, item.Url))
                    {
                        _model = item;
                        break;
                    }
                }
            }


            if (_model != null)
            {
                ViewBag.Url = _model.Url;
                ViewBag.Expression = _model.Expression;
                ViewBag.Title = _model.PageTitle;
                ViewBag.Description = _model.Description;
                ViewBag.Keywords = _model.Keywords;

                Dictionary<string, string> _dict = new Dictionary<string, string>();
                GenerateBreadCrumbData(_model, _dict);
                ViewBag.BreadCrumb = _dict;
            }
            else
            {

            }

            return View(_model);
        }

        private void GenerateBreadCrumbData(Page _page, Dictionary<string, string> _data)
        {
            if (_page.Parent != null)
            {
                GenerateBreadCrumbData(_page.Parent, _data);
            }

            _data.Add(Url.Action("Index", "Page", new { url = _page.Url }), _page.PageTitle);
        }
    }
}