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

        public static string GenerateClientTemplateHyperLink(string title, string action, string controller, object key, string areaName)
        {
            var _culture = BITC.Web.Library.CultureHelper.GetCurrentCulture();

            return string.Format("<a href='/{0}/{1}/{2}/{3}/#={4}#'>#={5}#</a>", areaName, _culture, controller, action, key, title);

            //return string.Format("<a href='/Admin/" + BITC.Web.Library.CultureHelper.GetCurrentCulture() + "/Page/Edit/#=PageID#'>#=PageTitle#</a>");
        }

        public static string GenerateClientTemplateUrl(string action, string controller, string key, string areaName)
        {
            var _culture = BITC.Web.Library.CultureHelper.GetCurrentCulture();

            return string.Format("/{0}/{1}/{2}/{3}/#={4}#", areaName, _culture, controller, action, key);
        }

        public static void Quicksort(IComparable[] elements, int left, int right)
        {
            int i = left, j = right;
            IComparable pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    IComparable tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }
 

        #endregion
    }
}
