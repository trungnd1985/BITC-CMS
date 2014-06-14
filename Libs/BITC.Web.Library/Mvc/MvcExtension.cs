using BITC.Web.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Xml.Linq;

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

        public static string Localize(this HtmlHelper helper, string key)
        {
            var _currentCulture = CultureHelper.GetCurrentCulture();
            var _filePath = helper.ViewContext.HttpContext.Server.MapPath("~/Language/" + _currentCulture + ".xml");

            XDocument _doc = XDocument.Load(_filePath);
            XElement _ele = _doc.Descendants().Where(i => i.Name.LocalName == "language" && i.Attribute("key").Value == key).FirstOrDefault();
            
            if (_ele == null)
            {
                return string.Empty;
            }

            return _ele.Value;
        }

        //public static string LocalResource(this HtmlHelper helper, string key)
        //{
        //    //var _virtualPath = helper.ViewContext
        //}
    }
}
