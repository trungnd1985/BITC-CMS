using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Hierarchy;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public interface IPageService : IService<Page>
    {
        Page GetPage(int id);

        void Update(int id, Page model);

        IQueryable<Page> FindByCulture(string _culture);

        HierarchyId GetCurrentPath(int? id);

        IQueryable<Page> GetDataForPageParentDropDownList(int? id);

        
    }
}
