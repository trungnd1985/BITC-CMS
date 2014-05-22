using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace System.Web.Mvc
{
    public static class MvcExtension
    {
        
        public static bool IncludeSetting(this HtmlHelper helper)
        {
            //var _controller = helper.ViewContext.Controller.ControllerContex
            //helper.ViewContext.View
            var _controllerContext = helper.ViewContext.Controller.ControllerContext;
            var _partialView = ViewEngines.Engines[0].FindPartialView(_controllerContext, "Setting", false);
            return (_partialView.View != null);
        }

        //public static string LocalResource(this HtmlHelper helper, string key)
        //{
        //    //var _virtualPath = helper.ViewContext
        //}
    }
}
