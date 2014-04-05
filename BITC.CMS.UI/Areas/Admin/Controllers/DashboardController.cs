using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        //
        // GET: /Admin/Dashboard/
        public ActionResult Index()
        {
            return View();
        }
    }
}