using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BITC.Web.Library.Mvc
{
    public class BitcRazorViewEngine : RazorViewEngine
    {

        public BitcRazorViewEngine()
            : base()
        {
            string _theme = GetTheme();

            AreaViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaMasterLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };
            AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };

            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml",

                "~/Theme/" + _theme + "/Views/{1}/{0}.cshtml",
                "~/Theme/" + _theme + "/Views/{1}/{0}.vbhtml",
                "~/Theme/" + _theme + "/Views/Shared/{0}.cshtml",
                "~/Theme/" + _theme + "/Views/Shared/{0}.vbhtml",
            };
            MasterLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml",
                
                "~/Theme/" + _theme + "/Views/{1}/{0}.cshtml",
                "~/Theme/" + _theme + "/Views/{1}/{0}.vbhtml",
                "~/Theme/" + _theme + "/Views/Shared/{0}.cshtml",
                "~/Theme/" + _theme + "/Views/Shared/{0}.vbhtml",
            };
            PartialViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.vbhtml",

                "~/Theme/" + _theme +  "/Views/{1}/{0}.cshtml",
                "~/Theme/" + _theme + "/Views/{1}/{0}.vbhtml",
                "~/Theme/" + _theme + "/Views/Shared/{0}.cshtml",
                "~/Theme/" + _theme + "/Views/Shared/{0}.vbhtml",
            };

            FileExtensions = new[]
            {
                "cshtml",
                "vbhtml",
            };
        }

        private string GetTheme()
        {
            var _th = System.Configuration.ConfigurationManager.AppSettings["Theme"];

            if (_th != null)
            {
                return _th.ToString();
            }

            return "Default";
        }

        //protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        //{

        //    var nameSpace = controllerContext.Controller.GetType().Namespace;
        //    return base.CreatePartialView(controllerContext, partialPath.Replace("%1", nameSpace));
        //}

        //protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        //{
        //    var nameSpace = controllerContext.Controller.GetType().Namespace;
        //    return base.CreateView(controllerContext, viewPath.Replace("%1", nameSpace), masterPath.Replace("%1", nameSpace));
        //}

        //protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        //{
        //    var nameSpace = controllerContext.Controller.GetType().Namespace;
        //    return base.FileExists(controllerContext, virtualPath.Replace("%1", nameSpace));
        //}

    }
}
