using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using BITC.Web.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BITC.CMS.Service
{
    public class MediaService : BaseService<Media>, IMediaService
    {
        #region Declaration

        private readonly IRepositoryAsync<Media> _repository;

        #endregion

        #region Constructor

        public MediaService(IRepositoryAsync<Media> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion

        public void Upload(IEnumerable<System.Web.HttpPostedFileBase> _upload)
        {
            var _mediaFolderPath = Utility.GetMediaFolderPhysicalPath();

            Media _medium = null;

            if (!Directory.Exists(_mediaFolderPath))
            {
                Directory.CreateDirectory(_mediaFolderPath);
            }

            foreach (var file in _upload)
            {
                // Some browsers send file names with full path.
                // We are only interested in the file name.
                var fileName = Path.GetFileName(file.FileName);
                var physicalPath = Path.Combine(_mediaFolderPath, fileName);

                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);

                _medium = new Media();
                _medium.FileName = fileName;
                _medium.FileSize = file.ContentLength;
                _medium.Extension = fileName.Substring(fileName.LastIndexOf('.'));
                _medium.UploadedDate = DateTime.Now;
                _medium.MIMEType = file.ContentType;
                _medium.Author = HttpContext.Current.User.Identity.Name;
                _medium.Url = Utility.GetMediaFolder() + "/" + fileName;
                this.Insert(_medium);
            }
        }
    }
}
