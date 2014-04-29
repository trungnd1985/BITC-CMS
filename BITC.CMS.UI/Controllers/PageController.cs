using BITC.CMS.Data;
using BITC.CMS.Data.Model;
using BITC.CMS.Repository;
using BITC.Library.Data;
using BITC.Web.Library.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BITC.CMS.UI.Controllers
{
    public class PageController : BitcController
    {
        public ActionResult Index(string url)
        {
            var _uow = new UnitOfWork();
            var _repo = _uow.GetRepository<Page>();
            if (string.IsNullOrEmpty(url))
            {
                url = "/";
            }
            var _model = _repo.SingleOrDefault(i => i.Url == url);

            if (_model !=null)
            {
                ViewBag.Title = _model.PageTitle;
                ViewBag.Description = _model.Description;
                ViewBag.Keywords = _model.Keywords;
            }
            
            return View(_model);
        }
    }
}