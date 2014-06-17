using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public interface IProjectService : IService<Project>
    {
        IQueryable<Project> FindByCulture(string _culture);

        IQueryable<Project> FindRecentProject(string _culture);

        IQueryable<Project> FindAvailableProject(string _culture);

        Project FindByKey(int id);

        void Update(int id, Project _model);
    }
}
