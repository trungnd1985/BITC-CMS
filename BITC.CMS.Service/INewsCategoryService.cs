using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public interface INewsCategoryService : IService<NewsCategory>
    {
        IQueryable<NewsCategory> FindByCulture(string _culture);

        IQueryable<NewsCategory> LoadAllNewsCategoryByTerm(string _culture, string _term);
    }
}
