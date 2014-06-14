using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BITC.CMS.Service
{
    public interface IMediaService : IService<Media>
    {
        void Upload(IEnumerable<HttpPostedFileBase> _upload);
    }
}
