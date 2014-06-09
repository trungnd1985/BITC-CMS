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

        IEnumerable<BlogEntry> GetBlogEntriesList(string _culture, string _tag, int _pageIndex, int _pageSize, out int _totalCount);

        IEnumerable<BlogEntry> GetRecentBlogEntriesList(string _culture, int _excludeId, int topCount);
    }
}
