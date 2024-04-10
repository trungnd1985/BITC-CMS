using BITC.Web.Library;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static HtmlString Scripts(this HtmlHelper helper)
        {
            //var _theme = Utility.GetTheme();
            var _manifestPath = Utility.GetManifestFile();
            string _scripts = string.Empty;

            if (File.Exists(_manifestPath))
            {
                XElement _xml = XElement.Load(_manifestPath);
                string template = helper.ViewBag.Template;

                var _template = _xml.Descendants("template").Where(i => i.Attribute("path").Value == template).Descendants("script");

                foreach (var item in _template)
                {
                    if (item.Attribute("src") != null)
                    {
                        _scripts += string.Format("<script type='{0}' src='{1}'></script>", item.Attribute("type").Value, item.Attribute("src").Value);
                    }
                    else
                    {
                        _scripts += string.Format("<script type='text/javascript'>{0}</script>",item.Value);
                    }

                }
            }

            return new HtmlString(_scripts);
        }

        public static HtmlString StyleSheets(this HtmlHelper helper)
        {
            //var _theme = Utility.GetTheme();
            var _manifestPath = Utility.GetManifestFile();
            string _scripts = string.Empty;

            if (File.Exists(_manifestPath))
            {
                XElement _xml = XElement.Load(_manifestPath);
                string template = helper.ViewBag.Template;

                var _template = _xml.Descendants("template").Where(i => i.Attribute("path").Value == template).Descendants("link");

                foreach (var item in _template)
                {
                    _scripts += string.Format("<link rel='{0}' href='{1}' />", item.Attribute("rel").Value, item.Attribute("href").Value);
                }
            }

            return new HtmlString(_scripts);
        }

        //public static string LocalResource(this HtmlHelper helper, string key)
        //{
        //    //var _virtualPath = helper.ViewContext
        //}
    }
}
