using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public interface IBlogService : IService<BlogEntry>
    {
        void Update(int id, BlogEntry entity);

        BlogEntry GetBlogEntry(int id);

        IQueryable<BlogEntry> FindByCulture(string _culture);
    }
}
