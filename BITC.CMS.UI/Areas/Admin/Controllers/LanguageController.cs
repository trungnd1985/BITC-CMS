using BITC.CMS.UI.Areas.Admin.Models;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class LanguageController : BitcController
    {
        // GET: Admin/Language
        public ActionResult Index()
        {
            return View();
        }

        #region Method

        private XDocument LoadLanguageFile()
        {
            var _filePath = GetLanguageFile();

            XDocument _doc = XDocument.Load(_filePath);
            return _doc;
        }

        private string GetLanguageFile()
        {
            var _currentCulture = CultureHelper.GetCurrentCulture();
            return HttpContext.Server.MapPath("~/Language/" + _currentCulture + ".xml");
        }

        #endregion

        #region AJAX

        [AjaxOnly]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            XDocument _doc = LoadLanguageFile();
            var lst = _doc.Descendants().Where(i => i.Name.LocalName == "language").Select(i => new LanguageModel() { Key = i.Attribute("key").Value, Value = i.Value }).AsQueryable();
            DataSourceResult _result = lst
                                .Where(request.Filters)
                                .Sort(request.Sorts)
                                .ToDataSourceResult(request);
            //_doc.Save()
            return Json(_result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, LanguageModel _model)
        {
            if (_model != null && ModelState.IsValid)
            {
                var _doc = LoadLanguageFile();

                var newElement = new XElement(XName.Get("language", "http://bitc.com.vn/language.xsd"));
                XAttribute key = new XAttribute("key", _model.Key);
                newElement.Add(key);
                newElement.SetValue(_model.Value);
                _doc.Element(XName.Get("languages", "http://bitc.com.vn/language.xsd")).Add(newElement);
                _doc.Save(GetLanguageFile());
            }

            return Json(new[] { _model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, LanguageModel _model)
        {
            if (_model != null && ModelState.IsValid)
            {
                var _doc = LoadLanguageFile();

                var _element = _doc.Descendants().FirstOrDefault(i => i.Name.LocalName == "language" && i.Attribute("key").Value == _model.Key);

                if (_element != null)
                {
                    _element.SetValue(_model.Value);

                    _doc.Save(GetLanguageFile());
                }
            }

            return Json(new[] { _model }.ToDataSourceResult(request, ModelState));
        }

        [AjaxOnly]
        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, LanguageModel _model)
        {
            var _doc = LoadLanguageFile();

            var _element = _doc.Descendants().FirstOrDefault(i => i.Name.LocalName == "language" && i.Attribute("key").Value == _model.Key);

            if (_element != null)
            {
                _element.Remove();

                _doc.Save(GetLanguageFile());
            }
            return Json(new[] { _model }.ToDataSourceResult(request, ModelState));
        }

        #endregion
    }
}