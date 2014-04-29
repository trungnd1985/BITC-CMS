using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BITC.Web.Library
{
    public class Utility
    {
        #region Declaration

        private const string KEY_THEME = "Theme";
        private const string MANIFEST_PATH_FORMAT = "~/Theme/{0}/manifest.xml";

        #endregion

        #region Method

        /// <summary>
        /// Get current Theme
        /// </summary>
        /// <returns></returns>
        public static string GetTheme()
        {
            var _th = WebConfigurationManager.AppSettings["Theme"];

            if (_th != null)
            {
                return _th.ToString();
            }

            return "Default";
        }

        /// <summary>
        /// Load Template files on current Theme
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetTemplate()
        {
            return GetTemplate(GetTheme());
        }

        /// <summary>
        /// Load Template files on Theme
        /// </summary>
        /// <param name="_theme"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetTemplate(string _theme)
        {
            List<SelectListItem> _templates = new List<SelectListItem>();

            _templates.Add(new SelectListItem() { Text = "- Select Template -", Value = "" });

            if (string.IsNullOrEmpty(_theme))
            {
                _theme = "Default";
            }

            var _manifestPath = HttpContext.Current.Server.MapPath(string.Format(MANIFEST_PATH_FORMAT, _theme));

            if (File.Exists(_manifestPath))
            {
                XElement _xml = XElement.Load(_manifestPath);

                foreach (var _node in _xml.Descendants("Template"))
                {
                    _templates.Add(new SelectListItem() { Text = _node.Attribute("name").Value, Value = _node.Attribute("path").Value });
                }
            }

            return _templates;
        }

        #endregion      
    }
}
