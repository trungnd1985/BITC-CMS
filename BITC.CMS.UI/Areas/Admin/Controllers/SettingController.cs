﻿using BITC.Web.Library.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class SettingController : BitcController
    {
        //
        // GET: /Admin/Setting/
        public ActionResult Index()
        {
            return View();
        }
	}
}