﻿using BITC.CMS.UI.Migrations;
using BITC.Web.Library.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BITC.CMS.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();   
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new BitcRazorViewEngine());
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
        }
    }
}
