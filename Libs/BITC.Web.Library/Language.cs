using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace BITC.Web.Library
{
    public class Language
    {
        public static string Localize(string key)
        {
            var _currentCulture = CultureHelper.GetCurrentCulture();
            var _filePath = HttpContext.Current.Server.MapPath("~/Language/" + _currentCulture + ".xml");

            XDocument _doc = XDocument.Load(_filePath);
            XElement _ele = _doc.Descendants().Where(i => i.Name.LocalName == "language" && i.Attribute("key").Value == key).FirstOrDefault();
            
            if (_ele == null)
            {
                return string.Empty;
            }

            return _ele.Value;
        }
    }
}
