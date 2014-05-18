using BITC.CMS.Data.Model;
using BITC.CMS.Repository;
using BITC.Web.Library;
using BITC.Web.Library.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BITC.CMS.UI.Areas.Admin.Controllers
{
    public class MediaController : BitcController
    {
        #region Declaration

        private const string MEDIA_FOLDER_KEY = "MediaFolder";
        private const string DEFAULT_MEDIA_FOLDER = "~/Upload";
        #endregion

        #region Action

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MediaPopup()
        {
            return PartialView("_MediaPopup");
        }

        public ActionResult MediaView()
        {
            return PartialView("_MediaView");
        }

        public ActionResult YoutubeView()
        {
            return PartialView("_YoutubeView");
        }

        public ActionResult UrlView()
        {
            return PartialView("_UrlView");
        }

        #endregion

        #region AJAX

        [HttpPost]
        public ActionResult GetMediaFiles()
        {
            using (var _unitOfWork = new UnitOfWork())
            {
                var _repo = _unitOfWork.GetRepository<Medium>();

                var lst = _repo.Query();

                return Json(lst.ToList());
            }
        }

        public ActionResult Upload(IEnumerable<HttpPostedFileBase> MediaUploadFile)
        {
            if (MediaUploadFile != null)
            {
                var _mediaFolderPath = Utility.GetMediaFolderPhysicalPath(this.HttpContext);
                using (var _unitOfWork = new UnitOfWork())
                {
                    var _repo = _unitOfWork.GetRepository<Medium>();
                    Medium _medium = null;

                    if (!Directory.Exists(_mediaFolderPath))
                    {
                        Directory.CreateDirectory(_mediaFolderPath);
                    }

                    foreach (var file in MediaUploadFile)
                    {
                        // Some browsers send file names with full path.
                        // We are only interested in the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(_mediaFolderPath, fileName);

                        // The files are not actually saved in this demo
                        file.SaveAs(physicalPath);

                        _medium = new Medium();
                        _medium.FileName = fileName;
                        _medium.FileSize = file.ContentLength;
                        _medium.Extension = fileName.Substring(fileName.LastIndexOf('.'));
                        _medium.UploadedDate = DateTime.Now;
                        _medium.MIMEType = file.ContentType;
                        _medium.Author = HttpContext.User.Identity.Name;
                        _medium.Url = Url.Content(Utility.GetMediaFolder(this.HttpContext) + "/" + fileName);
                        _repo.Insert(_medium);
                    }

                    _unitOfWork.SaveChange();
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        #endregion
    }
}