using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public interface IProjectCategoryService : IService<ProjectCategory>
    {
        IQueryable<ProjectCategory> FindByCulture(string _culture);
    }
}
