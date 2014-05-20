using BITC.CMS.Data.Entity;
using System;
using System.Threading;
using System.Web.Mvc;
using System.Linq;

namespace BITC.Web.Library.Mvc
{
    public class BitcController : Controller
    {
        #region Property

        public Guid ModuleID { get; set; }

        #endregion

        #region Method

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //using (var _unitOfWork = new UnitOfWork())
            //{
            //    var _settingRepo = _unitOfWork.GetRepository<Setting>();

            //    ViewBag.Settings = _settingRepo.Query(i => i.ModuleID == ModuleID).ToDictionary(i => i.SettingKey, i => i.SettingValue);

            //}

            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = RouteData.Values["culture"] as string;

            // Attempt to read the culture cookie from Request
            if (cultureName == null)
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            if (RouteData.Values["culture"] as string != cultureName)
            {
                // Force a valid culture in the URL
                RouteData.Values["culture"] = cultureName.ToLowerInvariant(); // lower case too

                // Redirect user
                Response.RedirectToRoute(RouteData.Values);
            }

            // Modify current thread's cultures           
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        #endregion

        #region Action

        //public virtual ActionResult Setting()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public virtual ActionResult Setting(Setting[] _settings)
        //{
        //    throw new NotImplementedException();
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion
    }
}
