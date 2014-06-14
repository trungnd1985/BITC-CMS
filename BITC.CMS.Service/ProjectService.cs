using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        #region Declaration

        private readonly IRepositoryAsync<Project> _repository;

        #endregion

        #region Constructor

        public ProjectService(IRepositoryAsync<Project> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion


        public IQueryable<Project> FindByCulture(string _culture)
        {
            return _repository.Queryable(i => i.Culture == _culture);
        }
    }
}
