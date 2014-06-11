using BITC.Web.Library.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    public class MenuController : BitcController
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
        }
    }
}