using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public interface INewsService : IService<News>
    {
        News GetNews(int id);

        IQueryable<News> FindByCulture(string _culture);

        void Update(int id, News _model);
    }
}
