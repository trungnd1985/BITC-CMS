using BITC.Web.Library.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    public class BlogController : BitcController
    {
        //
        // GET: /Admin/Blog/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tag()
        {
            return View();
        }

        #region AJAX



        #endregion
    }
}